using HttpModels;

namespace HttpApiServer_backend;

public interface ICartRepository : IRepository<Cart>
{
    Task<Cart> GetByAccountId(int accountId);
}