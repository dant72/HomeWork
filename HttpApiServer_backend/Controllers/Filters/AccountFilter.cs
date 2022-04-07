using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HttpApiServer_backend.Controllers
{
    public class AccountFilter : Attribute, IExceptionFilter
    {
        private readonly ILogger<AccountFilter> _logger;
        public AccountFilter(ILogger<AccountFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogInformation("Handle MVC exception");

            var message = TryGetMessageFromException(context);

            if (message != null)
            {
                context.Result = new ObjectResult(new { Message = message });
                context.ExceptionHandled = true;
            }
            else
            {
                context.Result = new ObjectResult(new { Message = "Неизвестная ошибка!" });
            }
        }

        private string? TryGetMessageFromException(ExceptionContext context)
        {
            return context.Exception switch
            {
                AccountException => "Такой логин или Email уже существует!",
                _ => null
            };
        }
    }
}
