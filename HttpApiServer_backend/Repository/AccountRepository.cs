using HttpModels;
using Microsoft.EntityFrameworkCore;

namespace HttpApiServer_backend;

public class AccountRepository : EfRepository<Account>, IAccountRepository
{
    public AccountRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public Task<Account> GetByEmail(string email)
        => _entities.FirstOrDefaultAsync(it => it.Email == email);
    
    public Task<Account> GetByLogin(string login)
        => _entities.FirstOrDefaultAsync(it => it.Login == login);
}
