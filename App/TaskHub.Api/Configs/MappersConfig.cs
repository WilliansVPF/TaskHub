using TaskHub.Application.Mappers;

namespace TaskHub.Api.Configs;

public static class MappersConfig
{
    public static void RegisterMappers(this IServiceCollection services)
    {
        services.AddScoped<UsuarioMapper>();
        services.AddScoped<TarefaMapper>();
        services.AddScoped<ProjetoMapper>();
    }
}