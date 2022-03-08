using HttpModels;
using Microsoft.EntityFrameworkCore;

namespace HttpApiServer_backend;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<Product> GetById(int id) 
        => _dbContext.Products.FirstAsync(it => it.Id == id);

    public Task<Product?> FindById(int id) 
        => _dbContext.Products.FirstOrDefaultAsync(it => it.Id == id);

    public async Task Add(Product product)
    {
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Product product)
    {
        _dbContext.Entry(product).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<IList<Product>> GetAll()
    {
        return await _dbContext.Products.ToListAsync();
    }
    
    public async Task GetTask(Product product)
    {
        _dbContext.Entry(product).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    } 
}