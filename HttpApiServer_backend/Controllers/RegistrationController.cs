using HttpApiClient;
using HttpModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HttpApiServer_backend.Controllers;

public class RegistrationController : ControllerBase
{
   private readonly IRegistrationService _registrationService;
   private readonly ILogger<RegistrationController> _logger;
   private readonly IPasswordHasher<Account> _passwordHasher;

   public RegistrationController(IRegistrationService registrationService, ILogger<RegistrationController> logger, IPasswordHasher<Account> passwordHasher)
   {
      _registrationService = registrationService;
      _logger = logger;
      _passwordHasher = passwordHasher;
   }
   
   [HttpPost]
   public async Task Registration([FromBody]AccountRequestModel account)
   {
      await _registrationService.AddAccount(Hash(account));
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

   private Account Hash(AccountRequestModel account)
   {
      var acc = new Account();
      string hashedPassword = _passwordHasher.HashPassword(acc, account.Password);

      acc.Id = 0;
      acc.Login = account.Login;
      acc.Email = account.Email;
      acc.Password = hashedPassword;

      return acc;
   }
}