using HttpModels;

namespace HttpApiServer_backend;

public interface IRegistrationService
{
    Task AddAccount(Account account);

    Task<Account> GetAccountByEmail(string email);

    Task<IReadOnlyList<Account>> GetAccounts();
}