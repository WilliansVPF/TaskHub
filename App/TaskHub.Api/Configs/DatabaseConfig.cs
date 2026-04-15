using Microsoft.EntityFrameworkCore;
using TaskHub.Infrastructure.Contexts;

namespace TaskHub.Api.Configs;

public static class DatabaseConfig
{
    public static void RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TaskHubContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")
            ));
    }
}