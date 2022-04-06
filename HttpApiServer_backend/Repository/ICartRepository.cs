using HttpModels;

namespace HttpApiServer_backend;

public interface ICartRepository : IRepository<Cart2>
{
    Task<Cart2> GetByAccountId(int accountId);
}