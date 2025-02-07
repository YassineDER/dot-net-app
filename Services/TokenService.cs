using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BackendDotNet.Models.Tables;
using BackendDotNet.Services.Interfaces;
using DotNetEnv;
using Microsoft.IdentityModel.Tokens;

namespace BackendDotNet.Services;

public class TokenService(IConfiguration config) : ITokenService
{
    private const int JWT_EXPIRY_DAYS = 7;

    public string CreateToken(AppUser user)
    {
        var tokenKey = config["JWT_SECRET"];

        if (string.IsNullOrEmpty(tokenKey))
            throw new EnvVariableNotFoundException("Cannot access the JWT secret key from the runtime environment.", "JWT_SECRET");
        if (tokenKey.Length < 64)
            throw new ArgumentException("The JWT secret key must be at least 64 characters long.");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
        var claims = new List<Claim> { new(ClaimTypes.NameIdentifier, user.UserName) };
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(JWT_EXPIRY_DAYS),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);

    }
}