using HttpModels;

namespace HttpApiServer_backend;

public interface ICartItemRepository : IRepository<CartItem>
{
    Task AddRange(List<CartItem> items);    
}