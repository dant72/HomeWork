using System.Diagnostics.Tracing;
using HttpModels;
using WebApplication3;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ISmtpEmailSender, SmtpEmailSender>();
builder.Services.AddSingleton<IClock, Clock>();

builder.Services.Configure<SmtpCredentials>(builder.Configuration.GetSection("SmtpCredentials"));
builder.Services.AddHostedService<BackService.ExampleBackgroundService>();

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .WriteTo.Sentry(builder.Configuration.GetSection("Dsn").Value)
    .WriteTo.File(builder.Configuration.GetSection("LogsPath").Value)
    .ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();