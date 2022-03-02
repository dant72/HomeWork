using System.Net.Mime;

namespace HttpModels;

public class Product
{
    public int Id { set; get; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; }
    
    public Category Category { get; set; }
    public Product(int id, string name, decimal price, Category category = null, string image = "")
    {
        Id = id;
        Name = name;
        Price = price;
        Image = image;
        Category = category;
    }

    public Product()
    {
    }

    public override string ToString()
    {
        return $"{Id} {Name} {Price}";
    }
}