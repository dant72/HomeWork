namespace HttpModels;

public class CartItem : IEntity
{
    public int Id { get; set; }
    public Product Product { get; set; }
    public int Count { get; set; }
    
    public CartItem() {}
}