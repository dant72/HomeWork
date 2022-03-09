using HttpModels;

namespace HttpApiClient;

public interface ICatalogSrv
{
    Task<IList<Product>> GetProducts();
    Task<IList<Category>> GetCategories();
    Task AddProduct(Product product);
    Task GetProduct(int id); 
}