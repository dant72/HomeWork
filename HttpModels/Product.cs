namespace HttpModels;

public class Product : IEntity
{
    public int Id { set; get; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string? Image { get; set; }
    
    public int CategoryId { get; set; }
    public Product(int id, string name, decimal price, int categoryId = 0, string? image = "")
    {
        Id = id;
        Name = name;
        Price = price;
        Image = image;
        CategoryId = categoryId;
    }

    public Product()
    {
    }

    public override string ToString()
    {
        return $"{Id} {Name} {Price}";
    }
}