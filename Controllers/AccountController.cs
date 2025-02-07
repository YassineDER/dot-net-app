using System.Security.Cryptography;
using System.Text;
using BackendDotNet.Data;
using BackendDotNet.Data.DTOs;
using BackendDotNet.Models.Tables;
using BackendDotNet.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendDotNet.Controllers;

public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
{

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registration)
    {
        if (await UserExists(registration.UserName))
            return BadRequest("Username is taken");

        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            UserName = registration.UserName.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registration.Password)),
            PasswordSalt = hmac.Key
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return Ok(new UserDto {
            Username = user.UserName,
            Token = tokenService.CreateToken(user)
        });
    }


    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto login)
    {
        var user = await context.Users.FirstOrDefaultAsync(x =>
            x.UserName == login.UserName.ToLower());

        if (Equals(user, null))
            return Unauthorized("Invalid username");

        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

        for (var i = 0; i < computedHash.Length; i++)
            if (computedHash[i] != user.PasswordHash[i])
                return Unauthorized("Invalid password");

        return Ok(new UserDto {
            Username = user.UserName,
            Token = tokenService.CreateToken(user)
        });
    }

    private async Task<bool> UserExists(string username)
    {
        return await context.Users.AnyAsync(x => x.UserName == username.ToLower());
    }

}