using HttpModels;

namespace HttpApiServer_backend;

public interface IRepository<TEntity> where TEntity: IEntity
{
    public Task<TEntity> GetById(int id);
    Task<IReadOnlyList<TEntity>> GetAll();
    Task Add(TEntity entity);
    Task Update(TEntity entity);
    Task Remove(TEntity entity);
}
