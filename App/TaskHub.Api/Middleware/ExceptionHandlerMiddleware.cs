using System.Text.Json;
using FluentValidation;
using TaskHub.Application.DTOs.Error;
using TaskHub.Application.Exceptions;

namespace TaskHub.Api.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JsonSerializerOptions _jsonSerializerOption;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
        _jsonSerializerOption = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException e)
        {
            await HandlerValidationExceptionAsync(context, e);
        }
        catch (IdentityCreationException e)
        {
            await HandlerIdentityCreationExceptionAsync(context, e);
        }
        catch (ResourceNotFoundException e)
        {
            await HandlerResourceNotFoundExceptionAsync(context, e);
        }
        catch (DataConflictException e)
        {
            await HandlerDataConflictExceptionAsync(context, e);
        }
        catch (BadCredentialsException e)
        {
            await HandlerBadCredentialsExceptionAsync(context, e);
        }
        catch (IdentityChangePasswordException e)
        {
            await HandlerIdentityChangePasswordExceptionAsync(context, e);
        }
        catch (UnauthorizedException e)
        {
            await HandlerUnauthorizedExceptionAsync(context, e);
        }
        catch (ForbiddenException e)
        {
            await HandlerForbiddenExceptionAsync(context, e);
        }
    }

    private Task HandlerValidationExceptionAsync(HttpContext context, ValidationException e)
    {
        var body = new ErrorResponseDTO
        {
          StatusCode = 400,
          Error = "Bad Request",
          Causa = e.GetType().Name,
          Mensagem = "Validation Error",
          TimeStamp = DateTime.Now,
          Errors = e.Errors.GroupBy(vf => vf.PropertyName).ToDictionary(g => g.Key, g => g.Select(vf => vf.ErrorMessage).ToArray())  
        };

        context.Response.StatusCode = body.StatusCode;
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(JsonSerializer.Serialize(body, _jsonSerializerOption));
    }

    private Task HandlerIdentityCreationExceptionAsync(HttpContext context, IdentityCreationException e)
    {
        var body = new ErrorResponseDTO
        {
          StatusCode = 400,
          Error = "Bad Request",
          Causa = e.GetType().Name,
          Mensagem = e.Message,
          TimeStamp = DateTime.Now,
          Errors = new Dictionary<string, string[]>
          {
              {"Identity", e.Errors.ToArray()}
          }  
        };

        context.Response.StatusCode = body.StatusCode;
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(JsonSerializer.Serialize(body, _jsonSerializerOption));
    }

    private Task HandlerResourceNotFoundExceptionAsync(HttpContext context, ResourceNotFoundException e)
    {
        var body = new ErrorResponseDTO
        {
            StatusCode = 404,
            Error = "Not Found",
            Causa = e.GetType().Name,
            Mensagem = e.Message,
            TimeStamp = DateTime.Now
        };

        context.Response.StatusCode = body.StatusCode;
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(JsonSerializer.Serialize(body, _jsonSerializerOption));
    }

    private Task HandlerDataConflictExceptionAsync(HttpContext context, DataConflictException e)
    {
        var body = new ErrorResponseDTO
        {
            StatusCode = 409,
            Error = "Conflict",
            Causa = e.GetType().Name,
            Mensagem = e.Message,
            TimeStamp = DateTime.Now
        };

        context.Response.StatusCode = body.StatusCode;
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(JsonSerializer.Serialize(body, _jsonSerializerOption));
    }

    private Task HandlerBadCredentialsExceptionAsync(HttpContext context, BadCredentialsException e)
    {
        var body = new ErrorResponseDTO
        {
            StatusCode = 401,
            Error = "Unauthorized",
            Causa = e.GetType().Name,
            Mensagem = e.Message,
            TimeStamp = DateTime.Now
        };

        context.Response.StatusCode = body.StatusCode;
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(JsonSerializer.Serialize(body, _jsonSerializerOption));
    }

    private Task HandlerIdentityChangePasswordExceptionAsync(HttpContext context, IdentityChangePasswordException e)
    {
        var body = new ErrorResponseDTO
        {
          StatusCode = 400,
          Error = "Bad Request",
          Causa = e.GetType().Name,
          Mensagem = e.Message,
          TimeStamp = DateTime.Now,
          Errors = new Dictionary<string, string[]>
          {
              {"Identity", e.Errors.ToArray()}
          }  
        };

        context.Response.StatusCode = body.StatusCode;
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(JsonSerializer.Serialize(body, _jsonSerializerOption));
    }

    private Task HandlerUnauthorizedExceptionAsync(HttpContext context, UnauthorizedException e)
    {
        var body = new ErrorResponseDTO
        {
            StatusCode = 401,
            Error = "Unauthorized",
            Causa = e.GetType().Name,
            Mensagem = e.Message,
            TimeStamp = DateTime.Now
        };

        context.Response.StatusCode = body.StatusCode;
        context.Response.ContentType = "application.json";
        return context.Response.WriteAsync(JsonSerializer.Serialize(body, _jsonSerializerOption));
    }

    private Task HandlerForbiddenExceptionAsync(HttpContext context, ForbiddenException e)
    {
        var body = new ErrorResponseDTO
        {
            StatusCode = 403,
            Error = "Forbidden",
            Causa = e.GetType().Name,
            Mensagem = e.Message,
            TimeStamp = DateTime.Now
        };

        context.Response.StatusCode = body.StatusCode;
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(JsonSerializer.Serialize(body, _jsonSerializerOption));
    }
}