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
            //var message = TryGetUserMessage
        }
    }
}
