using HttpApiServer_backend;
using HttpModels;


var dbPath = "myapp.db";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IClock, Clock>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddHttpClient<ICatalogService, CatalogService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();