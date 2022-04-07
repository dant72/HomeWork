namespace HttpModels;

public class Cart : IEntity
{
    public int Id { get; set; } = 0;
    public Account? Account { get; set; }
    public int AccountId { get; set; } = 0;

    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public Cart(int id, Account account)
    {
        Id = id;
        Account = account;
        //AccountId = account?.Id;
    }
    
    public Cart() { }
}