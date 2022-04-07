using HttpModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

    [HttpPost]
    public async Task AddProduct([FromBody]Product product)
    {
        await _catalog.AddProduct(product);
    }

    //[HttpPost]
    //public async Task AddCart([FromBody]Cart cart)
    //{
    //    await _catalog.AddCart(cart);
    //}
    
    [Authorize]
    [HttpGet]
    public async Task<Cart?> GetCart()
    {
        var accountId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        return await _catalog.GetCart(accountId);
    }
}