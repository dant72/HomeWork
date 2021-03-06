using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorApp_frontend;
using HttpApiClient;
using HttpModels;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<IClock, Clock>();
builder.Services.AddSingleton(_ => new ApiClient("https://localhost:7214"));

var app = builder.Build();

app.RunAsync();