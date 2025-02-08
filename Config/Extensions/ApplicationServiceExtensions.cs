using BackendDotNet.Data;
using BackendDotNet.Services;
using BackendDotNet.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackendDotNet.Config.Extensions;

public static class ApplicationServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();
        services.AddDbContext<DataContext>(options =>
            options.UseSqlite(config.GetConnectionString("DefaultConnection")));
        services.AddCors();
        services.AddScoped<ITokenService, TokenService>();
    }

}