using HttpModels;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace HttpApiServer_backend.Tests
{
    public class CatalogServiceTest
    {
        private readonly Mock<IClock> _clock;
        private readonly CatalogService _service;
        private readonly HttpClient _httpClient;
        private readonly IUnitOfWork _uow;
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly Mock<ICategoryRepository> _categoryRepository;
        private readonly SqliteConnection _connection;
        private readonly AppDbContext _context;
        public CatalogServiceTest()
        {
            _httpClient = new HttpClient();
            _clock = new Mock<IClock>();
            
            _connection = new SqliteConnection("DataSource=:memory:");
            
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(_connection)
                .Options;

            _context = new AppDbContext(options);

            _cartRepository = new CardRepository(_context);
            _cartItemRepository = new CartItemRepository(_context);
            _productRepository = new ProductRepository(_context);
            _accountRepository = new AccountRepository(_context);
            _categoryRepository = new Mock<ICategoryRepository>();
            
            _uow = new UnitOfWorkEf(_context, _accountRepository, _productRepository, _categoryRepository.Object, _cartRepository, _cartItemRepository);

            _service = new CatalogService(_clock.Object, _httpClient, _uow);
        }

        [Fact]
        public async Task New_item_added_to_cart()
        {
            var product = new Product() { Name = "test"};
            var acc = new Account() { Login = "test", Email = "test" };
            var cart = new Cart() { Account = acc };
            var cartItem = new CartItem() { Product = product, Cart = cart, Count = 1 };

            try
            {   
                await _connection.OpenAsync();
                await _context.Database.EnsureCreatedAsync(); 
                await _accountRepository.Add(acc);
                await _productRepository.Add(product);
                await _cartRepository.Add(cart);
                await _uow.SaveChangesAsync();

                await _service.AddCartItem(cartItem);
            }
            catch(Exception ex)
            {

            }

            var carts = await _service.GetCards();
            var result = carts.First().CartItems.First();

            await _connection.CloseAsync();
            await _context.DisposeAsync();

            Assert.Equal(result.ProductId, cartItem.Product.Id);
            Assert.Equal(result.Count, cartItem.Count);
        }

    }
}
