using TaskHub.Domain.DomainServices;

namespace TaskHub.Api.Configs;

public static class DomainServicesConfig
{
    public static void RegisterDomainServices(this IServiceCollection services)
    {
        services.AddScoped<TarefaDomainService>();
        services.AddScoped<ProjetoDomainService>();
    }
}