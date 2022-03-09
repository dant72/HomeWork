using HttpApiServer_backend;
using HttpModels;

namespace HttpApiClient;

public class CatalogSrv
{
    private readonly IClock _clock;
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    public List<Category> Categories { get; set; } = new();
    private readonly HttpClient _httpClient;

    public CatalogSrv(IClock clock, IProductRepository productRepository, ICategoryRepository categoryRepository, HttpClient httpClient)
    {
        _clock = clock;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _httpClient = httpClient;
    }


    public async Task<IList<Product>> GetProducts()
    {
        var pr = await _productRepository.GetAll(); 
        return pr.Select(p => new Product(p.Id, p.Name, p.Price * DayOfWeekPrice(_clock.LocalTime) * UserAgentPrice(""), p.CategoryId, p.Image)).ToList();
    }

    public async Task<IList<Category>> GetCategories()
    {
        return await _categoryRepository.GetAll();
    }
    
    public Task AddProduct(Product product)
    {
        _productRepository.Add(product);
        _productRepository.Add(new Product(1,"apple", 100, 1,"https://media.istockphoto.com/photos/red-apple-fruit-with-green-leaf-isolated-on-white-picture-id925389178?s=612x612")); 
        return Task.CompletedTask;

    }
    
    public Task GetProduct(int id)
    {
        return _productRepository.GetById(id);
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