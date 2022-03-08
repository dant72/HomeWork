using HttpModels;

namespace HttpApiServer_backend;

public interface ICatalogService
{
    Task<IList<Product>> GetProducts();
    Task<IList<Category>> GetCategories();

    Task AddProduct(Product product);
}