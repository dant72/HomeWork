using HttpApiServer_backend;
using HttpApiServer_backend.Controllers;
using HttpApiServer_backend.Controllers.Filters;
using HttpModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var dbPath = "myapp.db";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddSingleton<IClock, Clock>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddHttpClient<ICatalogService, CatalogService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddSingleton<IPasswordHasher<Account>, PasswordHasher<Account>>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWorkEf>();
builder.Services.AddScoped<ICartRepository, CardRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
builder.Services.AddScoped<AuthFilter>();


builder.Services.AddCors();

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite($"Data Source={dbPath}"));
builder.Services.AddControllers(optionts =>
{
    optionts.Filters.Add<AccountFilter>();
    optionts.Filters.Add<TimeFilter>();
});
JwtConfig jwtConfig = builder.Configuration
    .GetSection("JwtConfig")
    .Get<JwtConfig>();

builder.Services.AddScoped<ITokenService>(_ => new TokenService(jwtConfig, new Clock()));
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(jwtConfig.SigningKeyBytes),
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            RequireSignedTokens = true,
          
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidAudiences = new[] { jwtConfig.Audience },
            ValidIssuer = jwtConfig.Issuer
        };
    });
builder.Services.AddAuthorization();
//builder.Services.AddScoped<ITokenService, TokenService>();


var app = builder.Build();

app.UseMiddleware<RequestLoggingMiddleware>();
//app.UseMiddleware<CheckBrowserMiddleware>();

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
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Catalog}/{action=Products}/{id?}");

app.Run();