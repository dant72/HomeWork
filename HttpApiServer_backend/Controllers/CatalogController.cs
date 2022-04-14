using HttpModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using HttpApiServer_backend.Controllers.Filters;

namespace HttpApiServer_backend.Controllers;

public class CatalogController : ControllerBase
{
    private ICatalogService _catalog;
    public CatalogController(ICatalogService catalog)
    {
        _catalog = catalog;
    }

    public Task<IReadOnlyList<Product>> Products()
    {
        return  _catalog.GetProducts();
    }
    
    public Task<IReadOnlyList<Category>> Categories()
    {
        return  _catalog.GetCategories();
    }

    public Task<IReadOnlyList<Cart>> Carts()
    {
        return _catalog.GetCards();
    }

    public Task<IReadOnlyList<CartItem>> CartItems()
    {
        return _catalog.GetCardItems();
    }

    [HttpPost]
    public async Task AddProduct([FromBody]Product product)
    {
        await _catalog.AddProduct(product);
    }

    [HttpPost]
    public async Task UpdateCart([FromBody]Cart cart)
    {
        await _catalog.UpdateCart(cart);
    }

    [HttpPost]
    public async Task AddCartItem([FromBody] CartItem cartItem)
    {
        await _catalog.AddCartItem(cartItem);
    }

    [HttpPost]
    public async Task UpdateCartItem([FromBody] CartItem cartItem)
    {
        await _catalog.UpdateCartItem(cartItem);
    }

    [HttpPost]
    public async Task RemoveCartItem([FromBody] CartItem cartItem)
    {
        await _catalog.RemoveCartItem(cartItem);
    }

    [Authorize]
    [HttpGet]
    public async Task<Cart?> GetCart()
    {
        var accountId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        return await _catalog.GetCart(accountId);
    }
}