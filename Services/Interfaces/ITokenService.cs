using BackendDotNet.Models.Tables;

namespace BackendDotNet.Services.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}