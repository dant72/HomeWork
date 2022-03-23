using HttpModels;

namespace HttpApiServer_backend;

public interface IAccountRepository : IRepository<Account>
{
    Task<Account> GetByEmail(string email);
    Task<Account> GetByLogin(string login);
}