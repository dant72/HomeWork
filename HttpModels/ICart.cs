namespace HttpModels;

public interface ICart
{
    public Dictionary<Product, int> Products { get; set; }
    public Task AddAsync(Product product);
    public Task RemoveAsync(Product product);
}