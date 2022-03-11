using HttpModels;

namespace HttpApiServer_backend;

public interface IProductRepository
{
    Task<Product> GetById(int id);
    Task Add(Product order);
    Task Update(Product order);
    Task<IReadOnlyList<Product>> GetAll();
}