using HttpModels;

namespace HttpApiServer_backend;

public interface IRegistrationService
{
    Task AddAccount(Account product);

    Task<Account> GetAccountByEmail(string email);
}