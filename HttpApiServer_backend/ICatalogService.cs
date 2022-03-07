using HttpModels;

namespace WebServerDB;

public interface ICatalogService
{
    Task<IList<Product>> GetProducts();

    Task AddProduct(Product product);
}