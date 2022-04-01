using HttpModels;
using Microsoft.EntityFrameworkCore;

namespace HttpApiServer_backend;

public class CardRepository : EfRepository<Cart2>, ICartRepository
{
    public CardRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IReadOnlyList<Cart2>> GetAll()
    {
        return await _entities
            .Include("Account")
            .Include("Items")
            .ToListAsync();
    }
}