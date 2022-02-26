using System.Collections.Concurrent;
using System.Net.Mime;

namespace HttpModels;

public class Catalog : ICatalog
{
    private ConcurrentBag<Product> Products { get; set; } = new ConcurrentBag<Product>();
     public List<Category> Categories { get; set; } = new();

     private readonly IClock _time;

    public Catalog(IClock time)
    {
        _time = time;
        Categories.Add(new Category(0,"Fruits"));
        Categories.Add(new Category(1, "Vegetables"));
        Products.Add(new Product(GetNewId(),"apple", 100, Categories[0],"https://media.istockphoto.com/photos/red-apple-fruit-with-green-leaf-isolated-on-white-picture-id925389178?s=612x612"));
        Products.Add(new Product(GetNewId(),"banana", 200, Categories[0],"https://media.istockphoto.com/photos/banana-picture-id1184345169?s=612x612"));
        Products.Add(new Product(GetNewId(),"orange", 150,Categories[0], "https://media.istockphoto.com/photos/whole-cross-section-and-quarter-of-fresh-organic-navel-orange-with-picture-id1227301369?s=612x612"));
        Products.Add(new Product(GetNewId(),"corn", 50, Categories[1], "https://media.istockphoto.com/photos/fresh-corn-with-green-leaves-still-life-vegetables-picture-id597955650?s=612x612"));
    }

    public int GetNewId()
    {
        return Products.Count > 0 ? Products.Max(p => p.Id) + 1 : 1;
    }

    public void AddProduct(Product product)
    {
        Products.Add(product);
    }

    public Product GetProductById(int id)
    {
        return Products.First(p => p.Id == id);
    }

    public void AddCategory(Category category)
    {
        Categories.Add(category);
    }

    public IEnumerable<Product> GetProducts(string userAgent)
    {
        return Products.Select(p => new Product(p.Id, p.Name, p.Price * DayOfWeekPrice(_time.LocalTime) * UserAgentPrice(userAgent), p.Category, p.Image));
    }
    
    private decimal DayOfWeekPrice(DateTime date)
    {
        switch (date.Date.DayOfWeek)
        {
            case DayOfWeek.Wednesday:
                return 1.5m;
            default:
                return 1.0m;
        }
    }
    
    private decimal UserAgentPrice(string userAgent)
    {
        switch (userAgent)
        {
            case "iPhone":
                return 1.5m;
            case "Window":
                return 1.1m;
            case "Android":
                return 0.9m;
            default: 
                return 1.0m;
        }
    }
}