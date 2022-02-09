namespace HttpModels;

public interface ICatalog
{
    public void AddProduct(Product product);
    public void AddCategory(Category category);

    public IEnumerable<Product> GetProducts(string userAgent);
    
    public List<Category> Categories { get; set; }
}