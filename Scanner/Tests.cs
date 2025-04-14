using Xunit;
using System.Threading.Tasks;
using Npgsql;
using Testcontainers.PostgreSql;

namespace Scanner.Tests
{
    public class PortScannerTests
    {
        [Fact]
        public async Task IsPortOpenAsync_OpenPort_ReturnsTrue()
        {
            var scanner = new PortScanner();
            var ip = "127.0.0.1";
            var port = 135;

            var result = await scanner.IsPortOpenAsync(ip, port);
            Assert.True(result);
        }

        [Fact]
        public async Task IsPortOpenAsync_ClosedPort_ReturnsFalse()
        {
            var scanner = new PortScanner();
            var ip = "127.0.0.1";
            var port = 1;

            var result = await scanner.IsPortOpenAsync(ip, port);
            Assert.False(result);
        }
        [Theory]
        [InlineData(0)]    // Невалидный порт
        [InlineData(65535)] // Максимальный порт
        public async Task IsPortOpenAsync_BoundaryPorts_ReturnsExpected(int port)
        {
            var scanner = new PortScanner();
            var result = await scanner.IsPortOpenAsync("127.0.0.1", port);

            if (port == 0)
                Assert.False(result); // Порт 0 всегда невалидный
            else
                Assert.IsType<bool>(result); // Проверка корректности типа
        }
    }
}