using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Text.Json;

namespace WebApplication1.Middleware
{
    internal class ExceptionMiddleware2
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware2> _logger;

        public ExceptionMiddleware2(RequestDelegate next,ILogger<ExceptionMiddleware2> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                httpContext.Response.ContentType = "application/problem+json";

                var title = "An error occured: " + ex.Message;
                var details = ex.ToString();
                //在该中间件中需要手动写错误日志
                _logger.LogError("{details}",details);

                var problem = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = title
                };

                httpContext.Response.StatusCode = StatusCodes.Status200OK;

                //Serialize the problem details object to the Response as JSON (using System.Text.Json)
                var stream = httpContext.Response.Body;
                await JsonSerializer.SerializeAsync(stream, problem);
            }
        }
    }
}
