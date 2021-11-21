using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net.Mime;
using System.Text.Json;

namespace WebApplication1.Filter
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger) => (_logger) = (logger);

        public Task OnExceptionAsync(ExceptionContext context)
        {
            _logger.LogInformation("{message}\t{trace}",context.Exception.Message,context.Exception.StackTrace);
            context.Result = new ContentResult
            {
                Content = JsonSerializer.Serialize(new { context.Exception.Message }),
                StatusCode = StatusCodes.Status200OK,
                ContentType = MediaTypeNames.Application.Json
            };
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
}
