using HttpModels;

namespace HttpApiServer_backend;

public class CartItemRepository : EfRepository<CartItem>, ICartItemRepository
{
    public CartItemRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public Task AddRange(List<CartItem> items)
    {
        _entities.AddRangeAsync(items);
        return Task.CompletedTask;  
    }
}