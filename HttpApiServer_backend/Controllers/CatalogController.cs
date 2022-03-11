using HttpModels;
using Microsoft.AspNetCore.Mvc;

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
    
    [HttpPost]
    public async Task AddProduct([FromBody]Product product)
    {
        await _catalog.AddProduct(product);
    }
}