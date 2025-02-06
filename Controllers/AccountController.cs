using System.Security.Cryptography;
using System.Text;
using BackendDotNet.Data;
using BackendDotNet.Data.DTOs;
using BackendDotNet.Models.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendDotNet.Controllers;

public class AccountController(DataContext context) : BaseApiController
{

    [HttpPost("register")]
    public async Task<ActionResult<AppUser>> Register(RegisterDto registration)
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

        return Ok(user);
    }

    private async Task<bool> UserExists(string username)
    {
        return await context.Users.AnyAsync(x => x.UserName == username.ToLower());
    }

}