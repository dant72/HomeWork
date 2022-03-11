using HttpApiServer_backend;
using HttpModels;
using Microsoft.EntityFrameworkCore;

var dbPath = "myapp.db";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddSingleton<IClock, Clock>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddHttpClient<ICatalogService, CatalogService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddCors();

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite($"Data Source={dbPath}"));
builder.Services.AddControllers();
var app = builder.Build();

app.MapGet("/", () => "Hello service!");

app.UseCors(policy =>
{
    policy
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin =>
            origin is "https://localhost:7253"
                or "https://mysite.ru"
        )
        .AllowCredentials();
});

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Catalog}/{action=Products}/{id?}");

app.Run();