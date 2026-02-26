using FluentValidation;
using Microsoft.AspNetCore.Identity;
using TaskHub.Application.DTOs.User;
using TaskHub.Application.Mappers;
using TaskHub.Application.Services;
using TaskHub.Application.Validatos.User;
using TaskHub.Domain.Entities;
using TaskHub.Infrastructure.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<TaskHubContext>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<TaskHubContext>();

//registra validators
//Usuario
builder.Services.AddScoped<IValidator<RegistrarUsuarioDTO>, RegistrarUsuarioValidator>();

//registra services
builder.Services.AddScoped<UsuarioService>();

//registra Mappers
builder.Services.AddScoped<UsuarioMapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
