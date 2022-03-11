using HttpModels;

namespace HttpApiServer_backend;

public interface ICategoryRepository
{

        Task<Category> GetById(int id);
        Task Add(Category order);
        Task Update(Category order);
        
        Task<IReadOnlyList<Category>> GetAll();

        
}