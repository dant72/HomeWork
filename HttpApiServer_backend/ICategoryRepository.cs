using HttpModels;

namespace HttpApiServer_backend;

public interface ICategoryRepository
{

        Task<Category> GetById(int id);
        Task Add(Category category);
        Task Update(Category category);
        
        Task<IReadOnlyList<Category>> GetAll();

        
}