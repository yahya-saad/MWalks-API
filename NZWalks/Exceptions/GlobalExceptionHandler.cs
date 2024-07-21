using System.Diagnostics;

namespace MWalks.API.Exceptions
{
    public sealed class GlobalExceptionHandler : IExceptionHandler
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
            var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

            _logger.LogError(
                exception,
                "Exception occurred: {Message}. TraceId {TraceId}",
                exception.Message,
                traceId);

            var (statusCode, title, errors) = MapException(exception);

            var problemDetails = new ProblemDetails
            {
                Title = title,
                Status = statusCode,
                Extensions = { ["traceId"] = traceId }
            };

            if (errors != null && errors.Any())
            {
                problemDetails.Extensions["errors"] = errors;
            }

            await Results.Problem(
                title: title,
                statusCode: statusCode,
                extensions: new Dictionary<string, object?>
                {
                    { "traceId", traceId },
                    { "errors", errors }
                })
                .ExecuteAsync(httpContext);

            return true;
        }

        static (int StatusCode, string Title, string[] Errors) MapException(Exception exception)
        {
            return exception switch
            {
                BadRequestException bre => (StatusCodes.Status400BadRequest, exception.Message, bre.Errors),
                UnauthorizedException => (StatusCodes.Status401Unauthorized, exception.Message, null),
                NotFoundException => (StatusCodes.Status404NotFound, exception.Message, null),
                _ => (StatusCodes.Status500InternalServerError, "Internal Server Error", null)
            };
        }
    }
}
