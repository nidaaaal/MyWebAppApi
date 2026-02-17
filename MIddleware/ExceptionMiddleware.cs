using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MyWebAppApi.DTOs;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyWebAppApi.MIddleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _host;

        public ExceptionMiddleware(RequestDelegate next,IHostEnvironment host,ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _host = host;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");

                await HandleExceprion(httpContext, ex);
            }
        }

        private async Task HandleExceprion(HttpContext context,Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = ApiResponseBuilder.Fail<string>(_host.IsDevelopment() ? exception.Message : "An unexpected error occurred.", context.Response.StatusCode);
            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
