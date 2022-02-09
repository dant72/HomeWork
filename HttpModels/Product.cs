using System.Net.Mime;

namespace HttpModels;

public class Product
{
    private static int currentID = 0;
    public int Id { set; get; }
    public string Name { get; set; }

    public decimal Price { get; set; }
    
    public string Image { get; set; }
    
    public Category Category { get; set; }
    
    public Product(string name, decimal price, Category category = null, string image = "")
    {
        Id = currentID++;
        Name = name;
        Price = price;
        Image = image;
        Category = category;
    }

    public override string ToString()
    {
        return $"{Id} {Name} {Price}";
    }
    
}