using HttpApiClient;
using HttpModels;
using Microsoft.AspNetCore.Mvc;

namespace HttpApiServer_backend;

public interface IRegistrationService
{
    Task AddAccount(Account account);
    
    Task <ActionResult<AccountResponseModel>>Autorization(AccountRequestModel account);

    Task<Account> GetAccountByEmail(string email);

    Task<IReadOnlyList<Account>> GetAccounts();
    Task BanAccount(Account account);
}