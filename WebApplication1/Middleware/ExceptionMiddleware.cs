using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Text.Json;

namespace WebApplication1.Middleware
{
    internal class ExceptionMiddleware
    {
        internal static async Task ExceptionHandler(HttpContext httpContext, Func<Task> next)
        {
            //该信息由ExceptionHandlerMiddleware中间件提供，里面包含了ExceptionHandlerMiddleware中间件捕获到的异常信息。
            var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();
            var ex = exceptionDetails?.Error;

            if (ex != null)
            {

                using var scope = httpContext.RequestServices.CreateScope();

                var logger = scope.ServiceProvider.GetRequiredService<ILogger<ExceptionMiddleware>>();

                httpContext.Response.ContentType = MediaTypeNames.Application.Json;

                var title = "An error occured: " + ex.Message;

                var problem = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = title
                };

                var stream = httpContext.Response.Body;
                await JsonSerializer.SerializeAsync(stream, problem);
            }
        }
    }
}
