namespace HttpApiServer_backend;

public class UnitOfWorkEf : IUnitOfWork, IAsyncDisposable
{
    public IAccountRepository AccountRepository { get; }
    public IProductRepository ProductRepository { get; }

    private readonly AppDbContext _dbContext;

    public UnitOfWorkEf(
        AppDbContext dbContext,
        IAccountRepository accountRepository,
        IProductRepository productRepository) 
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        AccountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        ProductRepository = productRepository ?? throw new ArgumentException(nameof(productRepository)); 
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => _dbContext.SaveChangesAsync(cancellationToken);

    public void Dispose() => _dbContext.Dispose();
    public ValueTask DisposeAsync() => _dbContext.DisposeAsync();
}
