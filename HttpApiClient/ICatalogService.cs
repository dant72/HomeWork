using HttpModels;

namespace HttpApiClient;

public interface ICatalogService
{
    Task<IList<Product>> GetProducts();
}