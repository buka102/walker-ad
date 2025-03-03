
namespace Wa.Api.Middlewares
{
    public class CustomLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomLoggingMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public CustomLoggingMiddleware(RequestDelegate next, ILogger<CustomLoggingMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (_env.IsDevelopment())
            {
                _logger.LogInformation($"[DEV] Request: {context.Request.Method} {context.Request.Path}");
            }
            else
            {
                _logger.LogInformation("[PROD] Request Received");
            }

            await _next(context);

            if (_env.IsDevelopment())
            {
                _logger.LogInformation($"[DEV] Response: {context.Response.StatusCode}");
            }
            else
            {
                _logger.LogInformation("[PROD] Response Sent");
            }
        }
    }
}