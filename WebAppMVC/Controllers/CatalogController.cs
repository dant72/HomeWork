using System.Diagnostics;
using HttpModels;
using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Models;


namespace WebAppMVC.Controllers;

public class CatalogController : Controller
{
    private readonly ICatalog _catalog;

    public CatalogController(ICatalog catalog)
    {
        _catalog = catalog;
    }
    public IActionResult Catalog()
    {
        return View(_catalog);
    }
    
    public IActionResult Category(int catid, object obj = null)
    {
        var list = _catalog.GetProducts(Request.Headers.UserAgent).Where(i => i.Category.Id == catid).ToList(); 
        return View(list);
    }

    public IActionResult AddProduct()
    {
        if (HttpContext.Request.Method == "POST")
        {
            var id = HttpContext.Request.Form["categoryId"].ToString();
            var name = HttpContext.Request.Form["name"].ToString();
            var image = HttpContext.Request.Form["image"].ToString();
            _catalog.AddProduct(new Product(name, 0, _catalog.Categories.First(x => x.Id == int.Parse(id)), image));
        }
        

        return View(_catalog);
    }
    
    public IActionResult AddCategory()
    {
        if (HttpContext.Request.Method == "POST")
        {
            var name = HttpContext.Request.Form["name"].ToString();
            _catalog.AddCategory(new Category(_catalog.Categories.Max(i => i.Id) + 1, name));
        }
        

        return View(_catalog);
    }
    
}