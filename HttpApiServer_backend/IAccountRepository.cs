using HttpModels;

namespace HttpApiServer_backend;

public interface IAccountRepository
{
    Task<Account> GetById(int id);
    Task Add(Account account);
    Task Update(Account account);
        
    Task<IReadOnlyList<Account>> GetAll(); 
}