using HttpModels;

namespace HttpApiServer_backend;

public interface IProductRepository
{
    Task<Product> GetById(int id);
    Task Add(Product product);
    Task Update(Product product);
    Task<IReadOnlyList<Product>> GetAll();
}