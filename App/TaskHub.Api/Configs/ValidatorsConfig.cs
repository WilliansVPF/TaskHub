using FluentValidation;
using TaskHub.Application.DTOs.Auth;
using TaskHub.Application.DTOs.Projeto;
using TaskHub.Application.DTOs.Tarefa;
using TaskHub.Application.DTOs.User;
using TaskHub.Application.Validatos.Auth;
using TaskHub.Application.Validatos.Projeto;
using TaskHub.Application.Validatos.Tarefa;
using TaskHub.Application.Validatos.User;

namespace TaskHub.Api.Configs;

public static class ValidatorsConfig
{
    public static void RegisterValidators(this IServiceCollection services)
    {
        //Usuario
        services.AddScoped<IValidator<RegistrarUsuarioDTO>, RegistrarUsuarioValidator>();
        services.AddScoped<IValidator<EditarUsuarioDTO>, EditarUsuarioValidator>();

        //Auth
        services.AddScoped<IValidator<LoginDTO>, LoginValidator>();
        services.AddScoped<IValidator<AlterarSenhaDTO>, AlterarSenhaValidator>();

        //Tarefa
        services.AddScoped<IValidator<CadastrarTarefaDTO>, CadastraTarefaValidator>();
        services.AddScoped<IValidator<EditarTarefaDTO>, EditarTarefaValidator>();
        services.AddScoped<IValidator<AdicionarResponsavelDTO>, AdicionarResponsavelValidator>();

        //Projeto
        services.AddScoped<IValidator<CriarProjetoDTO>, CriarProjetoValidator>();
        services.AddScoped<IValidator<AdicionarMembroDTO>, AdicionarMembroValidator>();
    }
}