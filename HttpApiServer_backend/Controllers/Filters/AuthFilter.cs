using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HttpApiServer_backend.Controllers.Filters
{
    public class AuthFilter : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<AuthFilter>>();

            if (string.IsNullOrEmpty(context.HttpContext.Request.Headers.Authorization))
            {
                logger.LogInformation("User user isnot authorized");
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "User user isnot authorized"
                };
                return;
            }
            else
            {
                logger.LogInformation("User user is authorized");
                await next();
            }
        }
    }
}
