using HttpModels;
using WebServerDB;

namespace HttpApiClient;

public class CatalogService : ICatalogService
{

    private readonly IClock _clock;
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public CatalogService(IClock clock, IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _clock = clock;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }


    public Task<IList<Product>> GetProducts()
    {
        return _productRepository.GetAll();
    }
}