using HttpModels;

namespace HttpApiServer_backend;

public interface IProductRepository
{
    Task<Product> GetById(int id);
    Task<Product?> FindById(int id);

    Task Add(Product order);
    Task Update(Product order);
    Task<IList<Product>> GetAll();
}