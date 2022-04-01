using HttpModels;

namespace HttpApiServer_backend;

public class CatalogService : ICatalogService
{

    private readonly IClock _clock;
    private readonly IUnitOfWork _uow;
    public List<Category> Categories { get; set; } = new();
    private readonly HttpClient _httpClient;

    public CatalogService(IClock clock,  HttpClient httpClient, IUnitOfWork uow)
    {
        _clock = clock;
        _uow = uow;
        _httpClient = httpClient;
    }


    public async Task<IReadOnlyList<Product>> GetProducts()
    {
        var pr = await _uow.ProductRepository.GetAll(); 
        return pr.Select(p => new Product(p.Id, p.Name, p.Price * DayOfWeekPrice(_clock.LocalTime) * UserAgentPrice(""), p.CategoryId, p.Image)).ToList();
    }

    public async Task<IReadOnlyList<Category>> GetCategories()
    {
        return await _uow.CategoryRepository.GetAll();
    }

    public async Task<int> GeNextId()
    {
        var products = await _uow.ProductRepository.GetAll();
        return products.Max(i => i.Id) + 1;
    }

    public Task AddProduct(Product product)
    {
        _uow.ProductRepository.Add(product);
        _uow.SaveChangesAsync(); 
        return Task.CompletedTask;
    }
    
    public Task GetProduct(int id)
    {
        return _uow.ProductRepository.GetById(id);
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