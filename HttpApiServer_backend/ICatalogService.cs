using HttpModels;

namespace HttpApiServer_backend;

public interface ICatalogService
{
    Task<IList<Product>> GetProducts();

    Task AddProduct(Product product);
}