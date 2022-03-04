using HttpModels;

namespace WebServerDB;

public interface ICategoryRepository
{
    public interface IProductRepository
    {
        Task<Category> GetById(int id);
        Task<Category?> FindById(int id);

        Task Add(Category order);
        Task Update(Category order); 
    }
}