using Microsoft.AspNetCore.Identity;
using TaskHub.Domain.Entities;
using TaskHub.Infrastructure.Contexts;

namespace TaskHub.Api.Configs;

public static class IdentityConfig
{
    public static void RegisterIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<TaskHubContext>()
            .AddDefaultTokenProviders();
    }
}