using AutoPricing.Api.Exceptions;
using System.Net;
using System.Text.Json;

namespace AutoPricing.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException exception)
        {
            await WriteResponseAsync(
                context,
                HttpStatusCode.NotFound,
                exception.Message);
        }
        catch (ConflictException exception)
        {
            await WriteResponseAsync(
                context,
                HttpStatusCode.Conflict,
                exception.Message);
        }
        catch (UnauthorizedAccessException exception)
        {
            await WriteResponseAsync(
                context,
                HttpStatusCode.Unauthorized,
                exception.Message);
        }

        catch (Exception exception)
        {
            _logger.LogError(
                exception,
                "Ocorreu um erro inesperado.");

            await WriteResponseAsync(
                context,
                HttpStatusCode.InternalServerError,
                "Ocorreu um erro interno no servidor.");
        }
    }

    private static async Task WriteResponseAsync(
        HttpContext context,
        HttpStatusCode statusCode,
        string message)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var response = new
        {
            message
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }
}