using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Sample.Shared.Infrastructure.Exceptions;

namespace Sample.Bootstrapper.Exceptions;

internal sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        switch (exception)
        {
            case CustomException customException:
                await HandleCustomExceptionAsync(httpContext, customException);
                break;
            default:
                await HandleGenericExceptionAsync(httpContext, exception);
                break;
        }

        return true;
    }

    private async Task HandleCustomExceptionAsync(
        HttpContext context,
        CustomException exception)
    {
        _logger.LogWarning(exception, "Unhandled exception occurred: {RequestName}", exception.RequestName);

        var problemDetails = new ProblemDetails
        {
            Title = "Error occurred",
            Detail = exception.Message,
            Status = StatusCodes.Status400BadRequest,
            Instance = context.Request.Path,
            Extensions =
            {
                ["RequestName"] = exception.RequestName
            }
        };

        if (exception.Errors != null)
        {
            problemDetails.Extensions["Errors"] = exception.Errors
                .Select(e => e.Description)
                .ToList();
        }

        await WriteProblemDetailsAsync(context, problemDetails);
    }

    private async Task HandleGenericExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        _logger.LogError(exception, "Unhandled generic exception occurred.");

        var problemDetails = new ProblemDetails
        {
            Title = "Internal server error",
            Detail = exception.Message,
            Status = StatusCodes.Status500InternalServerError,
            Instance = context.Request.Path
        };

        await WriteProblemDetailsAsync(context, problemDetails);
    }

    private static async Task WriteProblemDetailsAsync(
        HttpContext context,
        ProblemDetails problemDetails)
    {
        context.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        var json = JsonSerializer.Serialize(problemDetails);
        await context.Response.WriteAsync(json);
    }
}