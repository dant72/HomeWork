using System.Collections.Concurrent;
using System.Net.Sockets;

namespace HttpModels;

public class Cart : ICart
{
    public Dictionary<Product, int> Products { get; set; }

    public Cart()
    {
        Products = new Dictionary<Product, int>();
    }

    public Task AddAsync(Product product)
    {
        if (Products.ContainsKey(product))
        {
            var pr = Products.First(p => p.Key == product);
            Products[product]++;
        }
        else
        {
            Products.Add(product, 0);
        } 
        
        return Task.CompletedTask;
    }

    public Task ClearAsync()
    {
        Products.Clear();
        return Task.CompletedTask;
    }
}