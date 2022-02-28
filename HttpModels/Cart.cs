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
            ++Products[product];
        }
        else
        {
            Products.Add(product, 1);
        } 
        
        return Task.CompletedTask;
    }
    
    public Task RemoveAsync(Product product)
    {
        if (Products.ContainsKey(product))
        {
            if (Products[product] > 1)
                --Products[product];
            else
                Products.Remove(product);
        } 
        
        return Task.CompletedTask;
    }

    public Task ClearAsync()
    {
        Products.Clear();
        return Task.CompletedTask;
    }
}