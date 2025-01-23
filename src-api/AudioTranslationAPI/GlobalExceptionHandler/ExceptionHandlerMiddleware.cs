namespace AudioTranslation.Api.GlobalExceptionHandler
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ArgumentException ex)
            {
                // Client-side error (e.g., invalid input)
                _logger.LogWarning(ex, "Client error occurred.");
                await HandleExceptionAsync(context, 400, "ERR001", "Invalid input provided.");
            }
            catch (HttpRequestException ex)
            {
                // External service error
                _logger.LogError(ex, "External service error occurred.");
                await HandleExceptionAsync(context, 502, "ERR002", "Failed to communicate with an external service.");
            }
            catch (Exception ex)
            {
                // Generic server-side error
                _logger.LogError(ex, "Unhandled exception occurred.");
                await HandleExceptionAsync(context, 500, "ERR500", "An internal server error occurred.");
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, int statusCode, string errorCode, string errorMessage)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var errorResponse = new
            {
                Code = errorCode,
                Message = errorMessage,
                Timestamp = DateTime.UtcNow
            };

            return context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
