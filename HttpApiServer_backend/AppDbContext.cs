using HttpModels;
using Microsoft.EntityFrameworkCore;

namespace HttpApiServer_backend;

public class AppDbContext : DbContext
{ 
   public DbSet<Category> Categories => Set<Category>();
   public DbSet<Product> Products => Set<Product>();


   public AppDbContext(
      DbContextOptions<AppDbContext> options) 
      : base(options)
   {
      if (!Database.EnsureCreated())
      {
         //CreateDB();
      }
      
   }

   private void CreateDB()
   {
      Categories.Add(new Category(1,"Fruits"));
      Categories.Add(new Category(2, "Vegetables"));
      Products.Add(new Product(1,"apple", 100, 1,"https://media.istockphoto.com/photos/red-apple-fruit-with-green-leaf-isolated-on-white-picture-id925389178?s=612x612"));
      Products.Add(new Product(2,"banana", 200, 1,"https://media.istockphoto.com/photos/banana-picture-id1184345169?s=612x612"));
      Products.Add(new Product(3,"orange", 150,1, "https://media.istockphoto.com/photos/whole-cross-section-and-quarter-of-fresh-organic-navel-orange-with-picture-id1227301369?s=612x612"));
      Products.Add(new Product(4,"corn", 50, 2, "https://media.istockphoto.com/photos/fresh-corn-with-green-leaves-still-life-vegetables-picture-id597955650?s=612x612"));
      SaveChanges();
   }

}