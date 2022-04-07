using HttpModels;
using Microsoft.EntityFrameworkCore;

namespace HttpApiServer_backend;

public class CardRepository : EfRepository<Cart>, ICartRepository
{
    public CardRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IReadOnlyList<Cart>> GetAll()
    {
        return await _entities
            //.Include("Account")
            //.Include("Items")
            .ToListAsync();
    }

    public Task<Cart> GetByAccountId(int accountId)
        => _entities.FirstOrDefaultAsync(it => it.AccountId == accountId);
}