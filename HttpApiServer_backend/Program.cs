using HttpApiServer_backend;
using HttpModels;
using Microsoft.EntityFrameworkCore;

var dbPath = "myapp.db";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddSingleton<IClock, Clock>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddHttpClient<ICatalogService, CatalogService>()
    .SetHandlerLifetime(TimeSpan.FromMinutes(5));

builder.Services.AddCors();

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
app.UseCors(policy =>
{
    policy
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin =>
            origin is "https://localhost:44383"
                or "https://mysite.ru"
        )
        .AllowCredentials();
});

app.Run();