using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WebServer.Utils
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        private readonly IHostingEnvironment _environment;

        public ErrorHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IHostingEnvironment environment)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = loggerFactory?.CreateLogger<ErrorHandlerMiddleware>();
            _environment = environment;
        }

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                if (httpContext.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the http status code middleware will not be executed.");
                    throw;

                }
                httpContext.Response.Clear();

                httpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;

                httpContext.Response.ContentType = "application/json";

                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new {
                    Error = new {
                        Code = httpContext.Response.StatusCode,
                        Message = _environment.IsProduction() ? "Unexpected error occurred" : ex.Message,
                        InnerError = ex.InnerException == null ? null : new {
                            Error = new {
                                Message = _environment.IsProduction() ? "Unexpected error occurred" : ex.Message,
                            }
                        }
                } }));

                return;
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ErrorHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}