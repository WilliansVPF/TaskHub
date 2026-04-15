using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace TaskHub.Api.Configs;

public static class AuthConfig
{
    public static void RegisterAuth(this IServiceCollection services, IConfiguration configuration)
    {

        var jwtKey = Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]!);

        services.AddAuthentication(x =>
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

    }
}