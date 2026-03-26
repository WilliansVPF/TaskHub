using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskHub.Application.Exceptions;
using TaskHub.Domain.Common;
using TaskHub.Domain.Entities;
using TaskHub.Domain.Enums;

namespace TaskHub.Api.Middleware;

public class UserValidationMiddleware
{
    private readonly RequestDelegate _next;

    public UserValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, UserManager<ApplicationUser> userManager)
    {

        var path = context.Request.Path.Value;
        bool isEnableUserRoute = path != null && path.Contains("/HabilitaUsuario", StringComparison.OrdinalIgnoreCase);
        
        if (isEnableUserRoute)
        {
            await _next(context);
            return;
        }

        // 1. Verifica se o usuário está autenticado
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                // 2. Check ultra-rápido usando AnyAsync + AsNoTracking
                var userExists = await userManager.Users
                    .AsNoTracking()
                    .AnyAsync(u => u.Id == userId && u.Ativo==true);

                if (!userExists)
                {
                    var result = Result.Failure("Usuário inexistente ou desativado.", ResultStatus.Unauthorized);
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsJsonAsync(result);
                    // await context.Response.WriteAsJsonAsync(new { message = "Usuário inexistente ou desativado." });
                    return;
                }
            }
        }

        await _next(context);
    }
}