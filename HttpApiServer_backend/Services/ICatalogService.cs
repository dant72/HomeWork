using HttpModels;

namespace HttpApiServer_backend;

public interface ICatalogService
{
    Task<IReadOnlyList<Product>> GetProducts();
    Task<IReadOnlyList<Category>> GetCategories();
    Task AddProduct(Product product);
    Task GetProduct(int id);
    Task<int> GeNextId();
    Task<IReadOnlyList<Cart>> GetCards();
    //Task AddCart(Cart cart);
    Task<Cart?> GetCart(int accountId);
}