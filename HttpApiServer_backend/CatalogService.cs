using HttpModels;

namespace HttpApiServer_backend;

public class CatalogService : ICatalogService
{

    private readonly IClock _clock;
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    public List<Category> Categories { get; set; } = new();
    private readonly HttpClient _httpClient;

    public CatalogService(IClock clock, IProductRepository productRepository, ICategoryRepository categoryRepository, HttpClient httpClient)
    {
        _clock = clock;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _httpClient = httpClient;
    }


    public Task<IList<Product>> GetProducts()
    {
        return _productRepository.GetAll();
        
    }
    
    public Task AddProduct(Product product)
    {
        _productRepository.Add(product);
        _productRepository.Add(new Product(1,"apple", 100, 1,"https://media.istockphoto.com/photos/red-apple-fruit-with-green-leaf-isolated-on-white-picture-id925389178?s=612x612")); 
        return Task.CompletedTask;

    }
}