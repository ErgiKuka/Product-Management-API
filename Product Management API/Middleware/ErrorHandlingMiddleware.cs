using System.ComponentModel.DataAnnotations;

namespace Product_Management_API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
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
            catch (ValidationException ex)
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                var errorResponse = new
                {
                    Message = "Validation failed.",
                    Details = ex.Message
                };
                _logger.LogWarning("Validation failed: {Details}", ex.Message);
                await context.Response.WriteAsJsonAsync(errorResponse);
            }
            catch (KeyNotFoundException ex)
            {
                context.Response.StatusCode = 404;
                context.Response.ContentType = "application/json";
                var errorResponse = new
                {
                    Message = "Resource not found.",
                    Details = ex.Message
                };
                _logger.LogWarning("Resource not found: {Details}", ex.Message);
                await context.Response.WriteAsJsonAsync(errorResponse);
            }
            catch (Exception ex)
            {
               context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var errorResponse = new
                {
                    Message = "An unexpected error occurred.",
                };
                _logger.LogError(ex, "An unexpected error occurred.");
                await context.Response.WriteAsJsonAsync(errorResponse);
            }

        }
    }
}
