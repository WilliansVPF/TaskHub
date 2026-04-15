using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using TaskHub.Api.Middleware;
using TaskHub.Api.Configs;
using TaskHub.Domain.Entities;
using TaskHub.Infrastructure.Contexts;

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

builder.Services.RegisterDatabase(builder.Configuration);

builder.Services.RegisterIdentity();

var jwtKey = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Secret"]!);

builder.Services.RegisterAuth(builder.Configuration);

//registra validators
builder.Services.RegisterValidators();

//registra services
builder.Services.RegisterServices();

//registra Domain Services
builder.Services.RegisterDomainServices();

//registra Mappers
builder.Services.RegisterMappers();

//registra Repositories
builder.Services.RegisterRepositories();


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
