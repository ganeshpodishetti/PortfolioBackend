using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Portfolio.Domain.Exceptions;

namespace Portfolio.API.Handlers;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    // 
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var (statusCode, message) = GetExceptionDetails(exception);
        logger.LogError(exception, exception.Message);
        
        httpContext.Response.StatusCode = (int)statusCode;
        await httpContext.Response.WriteAsJsonAsync(message, cancellationToken);

        return true;
    }
    
    private (HttpStatusCode statusCode, string message) GetExceptionDetails(Exception exception)
    {
        return exception switch
        {
            LoginFailedException => (HttpStatusCode.Unauthorized, exception.Message),
            UserAlreadyExistsException => (HttpStatusCode.Conflict, exception.Message),
            RegistrationFailedException => (HttpStatusCode.BadRequest, exception.Message),
            RefreshTokenException => (HttpStatusCode.Unauthorized, exception.Message),
            NotFoundException => (HttpStatusCode.NotFound, exception.Message),
            ForbidException => (HttpStatusCode.Forbidden, exception.Message),
            _ => (HttpStatusCode.InternalServerError, $"An unexpected error occurred: {exception.Message}")
        };
    }
}