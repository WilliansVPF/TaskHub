using TaskHub.Application.Services;

namespace TaskHub.Api.Configs;

public static class ServicesConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<UsuarioService>();
        services.AddScoped<AuthService>();
        services.AddScoped<TokenService>();
        services.AddScoped<TarefaService>();
        services.AddScoped<ProjetoService>();
    }
}