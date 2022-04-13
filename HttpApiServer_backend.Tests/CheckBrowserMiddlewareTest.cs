using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace HttpApiServer_backend.Tests
{
    public class CheckBrowserMiddlewareTest
    {
        private Mock<ILogger<RequestLoggingMiddleware>> _logger = new Mock<ILogger<RequestLoggingMiddleware>>();

        [Fact]
        public async void InvokeAsync_IsEdge_False()
        {
            var passed = false;
            var middleware = new CheckBrowserMiddleware(_ => { passed = true; return Task.CompletedTask; }, _logger.Object);

            var context = new DefaultHttpContext();
            context.Request.Headers.UserAgent = new []{ "Edg" };

            await middleware.InvokeAsync(context);

            Assert.False(passed);
        }
    }
}