using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.ExeptionHandlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError($"An error occurred while processing your request: {exception.Message}");
            var (statusCode, title) = MapException(exception);


            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = exception.Message
            };

            httpContext.Response.StatusCode = statusCode;

            await httpContext
                .Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        private static (int StatusCode, string Title) MapException(Exception exception)
        {
            return exception switch
            {
                UserNotFoundException => (StatusCodes.Status404NotFound, "User Not Found Exception"),
                RouteNotFoundException => (StatusCodes.Status404NotFound, "Route Not Found Exception"),
                _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")

            };
        }
    }
}
