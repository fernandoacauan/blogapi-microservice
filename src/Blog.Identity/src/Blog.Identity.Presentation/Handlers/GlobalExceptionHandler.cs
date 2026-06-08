using System.Text.Json;
using Blog.Identity.Application.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Identity.Presentation.Handlers;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        ProblemDetails problemDetails;

        _logger.LogInformation(exception, "Error: {Message}", exception.Message);

        httpContext.Response.ContentType = "application/problem+json";

        switch (exception)
        {
            case ForbiddenException ex:
                problemDetails = new()
                {
                    Title = "Forbidden",
                    Detail = ex.Message,
                    Status = StatusCodes.Status403Forbidden
                };
            break;
            case ValidationException ex:
                var errors = ex.Errors.GroupBy(e => e.PropertyName)
                                      .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
                var vProblems = new ValidationProblemDetails(errors)
                {
                    Title = "Validation Exception",
                    Detail = ex.Message,
                    Status = StatusCodes.Status400BadRequest
                };

                httpContext.Response.StatusCode = (int)vProblems.Status;
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(vProblems), cancellationToken);
                return true;
            case ConflictException ex:
                problemDetails = new()
                {
                    Title = "Conflict",
                    Detail = ex.Message,
                    Status = StatusCodes.Status409Conflict
                };
            break;
            case NotFoundException ex:
                problemDetails = new()
                {
                    Title = "Not Found",
                    Detail = ex.Message,
                    Status = StatusCodes.Status404NotFound
                };
            break;
            default: 
                problemDetails = new()
                {
                    Title = "Internal Error",
                    Detail = "Internal Server Error",
                    Status = StatusCodes.Status500InternalServerError
                };
            break;
        }

        httpContext.Response.StatusCode = (int)problemDetails.Status;
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(problemDetails), cancellationToken);

        return true;
    }
}
