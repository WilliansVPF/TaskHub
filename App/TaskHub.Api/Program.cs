using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TaskHub.Api.Middleware;
using TaskHub.Application.DTOs.Auth;
using TaskHub.Application.DTOs.User;
using TaskHub.Application.Mappers;
using TaskHub.Application.Services;
using TaskHub.Application.Validatos.Auth;
using TaskHub.Application.Validatos.User;
using TaskHub.Domain.Entities;
using TaskHub.Infrastructure.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<TaskHubContext>();
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
        ValidateAudience = false
    };
});

//registra validators
//Usuario
builder.Services.AddScoped<IValidator<RegistrarUsuarioDTO>, RegistrarUsuarioValidator>();
builder.Services.AddScoped<IValidator<EditarUsuarioDTO>, EditarUsuarioValidator>();

//Auth
builder.Services.AddScoped<IValidator<LoginDTO>, LoginValidator>();

//registra services
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<TokenService>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.Run();
