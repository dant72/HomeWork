using HttpModels;
using Microsoft.AspNetCore.Mvc;

namespace HttpApiServer_backend;

public class CatalogController : Controller
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

    public async Task Add(string name, string price, int categoryId, string img)
    {
        var product = new Product(0, name, Decimal.Parse(price), categoryId, img);
        await _catalog.AddProduct(product);
    }
}