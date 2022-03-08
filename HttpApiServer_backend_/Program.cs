using HttpModels;
using Microsoft.EntityFrameworkCore;
using WebServerDB;

var dbPath = "myapp.db";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddSingleton<IClock, Clock>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();


builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite($"Data Source={dbPath}"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/categories", async (AppDbContext context)
    => await context.Categories.ToListAsync());

app.MapGet("/products", async (AppDbContext context)
    => await context.Products.ToListAsync());

app.MapGet("/add", async (AppDbContext context)
    => await context.Products.ToListAsync());


app.Run();