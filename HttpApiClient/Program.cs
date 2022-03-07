using HttpModels;
using Microsoft.EntityFrameworkCore;
using WebServerDB;

var dbPath = "myapp.db";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICatalogService, CatalogService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();