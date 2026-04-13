using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using TaskHub.Api.Middleware;
using TaskHub.Application.DTOs.Auth;
using TaskHub.Application.DTOs.Projeto;
using TaskHub.Application.DTOs.Tarefa;
using TaskHub.Application.DTOs.User;
using TaskHub.Application.Mappers;
using TaskHub.Application.Services;
using TaskHub.Application.Validatos.Auth;
using TaskHub.Application.Validatos.Projeto;
using TaskHub.Application.Validatos.Tarefa;
using TaskHub.Application.Validatos.User;
using TaskHub.Domain.DomainServices;
using TaskHub.Domain.Entities;
using TaskHub.Domain.Interfaces;
using TaskHub.Domain.Interfaces.Repositories;
using TaskHub.Infrastructure.Contexts;
using TaskHub.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Token de autenticação."
    });

    option.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
       [new OpenApiSecuritySchemeReference("Bearer", document)] = [] 
    });
});

builder.Services.AddDbContext<TaskHubContext>(options => 
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<TaskHubContext>()
    .AddDefaultTokenProviders();

var jwtKey = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Secret"]!);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
    };
});

//registra validators
//Usuario
builder.Services.AddScoped<IValidator<RegistrarUsuarioDTO>, RegistrarUsuarioValidator>();
builder.Services.AddScoped<IValidator<EditarUsuarioDTO>, EditarUsuarioValidator>();

//Auth
builder.Services.AddScoped<IValidator<LoginDTO>, LoginValidator>();
builder.Services.AddScoped<IValidator<AlterarSenhaDTO>, AlterarSenhaValidator>();

//Tarefa
builder.Services.AddScoped<IValidator<CadastrarTarefaDTO>, CadastraTarefaValidator>();
builder.Services.AddScoped<IValidator<EditarTarefaDTO>, EditarTarefaValidator>();

//Projeto
builder.Services.AddScoped<IValidator<CriarProjetoDTO>, CriarProjetoValidator>();
builder.Services.AddScoped<IValidator<AdicionarMembroDTO>, AdicionarMembroValidator>();

//registra services
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<TarefaService>();
builder.Services.AddScoped<ProjetoService>();

//registra Domain Services
builder.Services.AddScoped<TarefaDomainService>();
builder.Services.AddScoped<ProjetoDomainService>();

//registra Mappers
builder.Services.AddScoped<UsuarioMapper>();
builder.Services.AddScoped<TarefaMapper>();
builder.Services.AddScoped<ProjetoMapper>();

//registra Repositories
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddScoped<IProjetoRepository, ProjetoRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
// {
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    // app.MapSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseMiddleware<UserValidationMiddleware>();

app.Run();
