using HttpModels;
using Microsoft.EntityFrameworkCore;

namespace WebServerDB;

public class AppDbContext : DbContext
{ 
   public DbSet<Category> Categories => Set<Category>();
   public DbSet<Product> Products => Set<Product>();
   

   public AppDbContext(
      DbContextOptions<AppDbContext> options) 
      : base(options)
   {
   }

}