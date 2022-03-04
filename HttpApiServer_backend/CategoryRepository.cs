using HttpModels;
using Microsoft.EntityFrameworkCore;

namespace WebServerDB;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _dbContext;
    
    public CategoryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<Category> GetById(int id) 
        => _dbContext.Categories.FirstAsync(it => it.Id == id);

    public Task<Category?> FindById(int id) 
        => _dbContext.Categories.FirstOrDefaultAsync(it => it.Id == id);

    public async Task Add(Category category)
    {
        await _dbContext.Categories.AddAsync(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Product category)
    {
        _dbContext.Entry(category).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }  
}