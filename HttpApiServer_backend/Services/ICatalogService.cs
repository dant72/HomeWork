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
    Task UpdateCart(Cart cart);
    Task<Cart?> GetCart(int accountId);
    Task<IReadOnlyList<CartItem>> GetCardItems();
    Task AddCartItem(CartItem cartItem);
    Task UpdateCartItem(CartItem cartItem);
    Task RemoveCartItem(CartItem cartItem);
}