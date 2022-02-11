using HttpModels;
using WebApplication3;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ISmtpEmailSender, SmtpEmailSender>();
builder.Services.AddSingleton<IClock, Clock>();

builder.Services.Configure<SmtpCredentials>(builder.Configuration.GetSection("SmtpCredentials"));
builder.Services.AddHostedService<BackService.ExampleBackgroundService>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();