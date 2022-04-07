namespace HttpApiServer_backend;

public interface IUnitOfWork : IDisposable
{
    IAccountRepository AccountRepository { get; }
    IProductRepository ProductRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    ICartRepository CartRepository { get; }
    ICartItemRepository CartItemRepository { get; }

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
