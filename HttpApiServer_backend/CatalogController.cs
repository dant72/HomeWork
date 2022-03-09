using HttpModels;
using Microsoft.AspNetCore.Mvc;

namespace HttpApiServer_backend;

public class CatalogController : ControllerBase
{
    private ICatalogService _catalog;
    public CatalogController(ICatalogService catalog)
    {
        _catalog = catalog;
    }

    public Task<IList<Product>> Products()
    {
        return  _catalog.GetProducts();
    }
    
    public Task<IList<Category>> Categories()
    {
        return  _catalog.GetCategories();
    }
    
    [HttpPost]
    public async Task AddProduct([FromBody]Product product)
    {
        await _catalog.AddProduct(product);
    }
}