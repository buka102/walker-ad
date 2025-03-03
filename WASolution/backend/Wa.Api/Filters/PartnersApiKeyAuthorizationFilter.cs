using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Wa.Api.Filters
{
    /// <summary>
    /// Action filter to validate API key authentication for third-party webhooks.
    /// </summary>
    public class PartnersApiKeyAuthorizationFilter : IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<PartnersApiKeyAuthorizationFilter> _logger;

        public PartnersApiKeyAuthorizationFilter(IConfiguration configuration, ILogger<PartnersApiKeyAuthorizationFilter> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("X-Api-Key", out var providedApiKey))
            {
                _logger.LogWarning("Missing API Key in webhook request.");
                context.Result = new UnauthorizedObjectResult("Missing API Key.");
                return;
            }

            var expectedApiKey = _configuration["WebhookSettings:ApiKey"];
            if (string.IsNullOrEmpty(expectedApiKey) || providedApiKey != expectedApiKey)
            {
                _logger.LogWarning("Invalid API Key provided by third party.");
                context.Result = new UnauthorizedObjectResult("Invalid API Key.");
            }
        }
    }
}