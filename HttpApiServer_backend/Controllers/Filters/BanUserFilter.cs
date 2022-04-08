using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace HttpApiServer_backend.Controllers.Filters
{
    public class BanUserFilter : Attribute, IActionFilter
    {
        private readonly ILogger<BanUserFilter> _logger;
        private readonly IRegistrationService _registrationService;

        public BanUserFilter(ILogger<BanUserFilter> logger, IRegistrationService registrationService)
        {
            _logger = logger;
            _registrationService = registrationService;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public async void OnActionExecuting(ActionExecutingContext context)
        {
            string? email = context.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            if (email != null)
            {
                var account = await _registrationService.GetAccountByEmail(email);
                if (account.IsBanned)
                {
                    context.Result = new ContentResult()
                    {
                        StatusCode = StatusCodes.Status403Forbidden,
                        Content = "User user is banned"
                    };
                    _logger.LogInformation($"{email} Forbidden!");
                }
            }
        }
    }
}
