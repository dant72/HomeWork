namespace HttpApiServer_backend;

public interface IUnitOfWork : IDisposable
{
    IAccountRepository AccountRepository { get; }
    IProductRepository ProductRepository { get; }
    ICategoryRepository CategoryRepository { get; }

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
