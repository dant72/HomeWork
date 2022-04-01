namespace HttpModels;

public class Cart2 : IEntity
{
    public int Id { get; set; }
    public Account Account { get; set; }
    public ICollection<CartItem> Items { get; set; } = new List<CartItem>();

    public Cart2(int id, Account account)
    {
        Id = id;
        Account = account;
    }
    
    public Cart2(){}
}