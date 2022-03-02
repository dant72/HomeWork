using Microsoft.EntityFrameworkCore;
using WebServerDB;
var dbPath = "myapp.db";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite($"Data Source={dbPath}"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");



app.MapGet("/categories", async (AppDbContext context)
    => await context.Categories.ToListAsync());

app.MapGet("/products", async (AppDbContext context)
    => await context.Products.ToListAsync());


app.Run();