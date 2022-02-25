namespace HttpModels;

public interface ICart
{
    public Dictionary<Product, int> Products { get; set; }
    
}