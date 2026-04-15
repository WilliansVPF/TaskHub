using TaskHub.Domain.Interfaces;
using TaskHub.Domain.Interfaces.Repositories;
using TaskHub.Infrastructure.Repositories;

namespace TaskHub.Api.Configs;

public static class RepositoriesConfig
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITarefaRepository, TarefaRepository>();
        services.AddScoped<IProjetoRepository, ProjetoRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}