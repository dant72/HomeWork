using HttpModels;

namespace HttpApiServer_backend;

public interface ICategoryRepository
{

        Task<Category> GetById(int id);
        Task<Category?> FindById(int id);

        Task Add(Category order);
        Task Update(Category order);
        
        Task<IList<Category>> GetAll();

        
}