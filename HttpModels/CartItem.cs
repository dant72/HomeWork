namespace HttpModels;

public class CartItem : IEntity
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int Count { get; set; }
    public int CartId {get;set;}

    public CartItem() {}
}