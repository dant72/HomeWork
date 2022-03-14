using HttpModels;
using Microsoft.AspNetCore.Mvc;

namespace HttpApiServer_backend.Controllers;

public class RegistrationController : ControllerBase
{
   private readonly IRegistrationService _registrationService;
   private readonly ILogger<RegistrationController> _logger;

   public RegistrationController(IRegistrationService registrationService, ILogger<RegistrationController> logger)
   {
      _registrationService = registrationService;
      _logger = logger;
   }
   
   [HttpPost]
   public async Task Registration([FromBody]Account account)
   {
      await _registrationService.AddAccount(account);
      _logger.Log(LogLevel.Information, $"Registration {account.Login}");
   }
   
   [HttpGet] 
   public async Task<Account> GetAccountByEmail(string email)
   {
      return  await _registrationService.GetAccountByEmail(email);
   }
   
   public Task<IReadOnlyList<Account>> Accounts()
   {
      return  _registrationService.GetAccounts();
   }
}