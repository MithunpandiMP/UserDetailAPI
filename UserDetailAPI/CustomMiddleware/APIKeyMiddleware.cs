using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using System.Net.Http;
using System.Threading.Tasks;

namespace UserDetailAPI.CustomMiddleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class APIKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private const string APIKEYNAME = "ApiKey";
        public APIKeyMiddleware(RequestDelegate next, ILoggerFactory logger)
        {
            _next = next;
            _logger = logger.CreateLogger("CustomAPIKeyMiddleware"); ;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            _logger.LogInformation("CustomAPIKeyMiddleware is processing..");
            if (!httpContext.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            {
                throw new UnauthorizedAccessException("Api Key was not provided");
            };
            string? apiKey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<string>("ApiKey");
            if (!apiKey.Equals(extractedApiKey))
            {
                throw new UnauthorizedAccessException("Api Key is not valid");
            }
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomAPIKeyMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomAPIKeyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<APIKeyMiddleware>();
        }
    }
}
