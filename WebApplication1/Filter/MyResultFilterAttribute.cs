using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication1.Filter
{
    public class MyResultFilter : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is ObjectResult objectResult)
            {
                context.Result = new ObjectResult(new { statusCode = StatusCodes.Status200OK, data = objectResult.Value });
            }
            await next();
        }
    }
}
