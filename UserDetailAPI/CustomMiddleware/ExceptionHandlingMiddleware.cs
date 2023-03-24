using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using UserDetailAPI.CustomMiddleware;
using KeyNotFoundException = UserDetailAPI.CustomMiddleware.KeyNotFoundException;
using BadRequestException = UserDetailAPI.CustomMiddleware.BadRequestException;
using NotFoundException = UserDetailAPI.CustomMiddleware.NotFoundException;
using UnauthorizedAccessException = UserDetailAPI.CustomMiddleware.UnauthorizedAccessException;
using NotImplementedException = UserDetailAPI.CustomMiddleware.NotImplementedException;
using static System.Net.Mime.MediaTypeNames;

namespace UserDetailAPI.CustomMiddleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;   
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);

            }
            catch (Exception ex)
            {
                httpContext.Response.ContentType = "application / json";
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private static  Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            HttpStatusCode statusCode;
            var stackTrace = string.Empty;
            string message;
            var exceptionType = exception.GetType();
            switch(exceptionType) {
                //new Pattern Matching implementation in switches in C#7
                case var type when type == typeof(BadRequestException):
                    statusCode = HttpStatusCode.BadRequest;
                    message = exception.Message;
                    stackTrace = exception.StackTrace;
                    break;
                    case var type when type == typeof(KeyNotFoundException): 
                    statusCode = HttpStatusCode.Unauthorized;
                    message = exception.Message;
                    stackTrace = exception.StackTrace;
                    break;
                    case var type when type == typeof(UnauthorizedAccessException):
                    statusCode = HttpStatusCode.Unauthorized;
                    message = exception.Message;
                    stackTrace = exception.StackTrace;
                    break;
                    case var type when type == typeof(NotFoundException):
                    statusCode = HttpStatusCode.NotFound;
                    message = exception.Message;
                    stackTrace = exception.StackTrace;
                    break;
                case var type when type == typeof(NotImplementedException):
                    statusCode = HttpStatusCode.NotImplemented;
                    message = exception.Message;
                    stackTrace = exception.StackTrace;
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError; 
                    message = exception.Message;
                    stackTrace = exception.StackTrace;
                    break;
            }
            var exceptionResult = JsonSerializer.Serialize(new
            {
                error = message, stackTrace
            });
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)statusCode;
            return httpContext.Response.WriteAsync(exceptionResult);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
