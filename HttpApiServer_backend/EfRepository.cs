using HttpModels;
using Microsoft.EntityFrameworkCore;

namespace HttpApiServer_backend;

public class EfRepository<TEntity> : IRepository<TEntity> where TEntity: class, IEntity
{
    protected readonly AppDbContext _dbContext;

    public EfRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private DbSet<TEntity> _entities => _dbContext.Set<TEntity>();

    public virtual Task<TEntity> GetById(int Id)
        => _entities.FirstAsync(it => it.Id == Id);

    public virtual async Task<IReadOnlyList<TEntity>> GetAll()
        => await _entities.ToListAsync();

    public virtual async Task Add(TEntity entity)
    {
        await _entities.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task Update(TEntity TEntity)
    {
        _dbContext.Entry(TEntity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }
}
