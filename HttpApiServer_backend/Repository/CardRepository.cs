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
            .Include("CartItems.Product")
            .Include("Account")
            .ToListAsync();
    }

    //public override async Task Update(Cart cart)
    //{
    //    _dbContext.Entry(cart).State = EntityState.Modified;
    //    _dbContext.Entry(cart).Navigations
    //}

    public Task<Cart?> GetByAccountId(int accountId)
        => _entities
        .Include("CartItems.Product")
        .FirstOrDefaultAsync(it => it.AccountId == accountId);
}