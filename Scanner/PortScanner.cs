using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Scanner
{
    public class PortScanner
    {
        public async Task<bool> IsPortOpenAsync(string ip, int port, int timeout = 3000)
        {
            using (var client = new TcpClient())
            using (var cts = new CancellationTokenSource())
            {
                try
                {
                    cts.CancelAfter(timeout);
                    await client.ConnectAsync(ip, port).WithCancellation(cts.Token);
                    return true;
                }
                catch (OperationCanceledException)
                {
                    return false; // Тайм-аут
                }
                catch (SocketException)
                {
                    return false; // Порт закрыт
                }
            }
        }
    }

    public static class TaskExtensions
    {
        public static async Task WithCancellation(this Task task, CancellationToken ct)
        {
            var tcs = new TaskCompletionSource<bool>();
            using (ct.Register(s => ((TaskCompletionSource<bool>)s).TrySetResult(true), tcs))
            {
                if (task != await Task.WhenAny(task, tcs.Task))
                {
                    throw new OperationCanceledException(ct);
                }
            }
            await task;
        }

    }
    public class FormPortScannerTests
    {
        [Fact]
        public async Task ButtonStartScan_Click_TriggersScanning()
        {
            // Arrange
            var form = new FormPortScanner();
            form.textBoxIP.Text = "127.0.0.1";
            form.textBoxStartPort.Text = "80";
            form.textBoxEndPort.Text = "81";

            // Act
            form.buttonStartScan.PerformClick();

            // Подождем немного, чтобы процесс сканирования начал выполняться
            await Task.Delay(500);

            // Assert
            Assert.False(form.buttonStartScan.Enabled); // Кнопка должна быть заблокирована
            Assert.Contains("Порт", form.listBoxResults.Items[0].ToString()); // В ListBox должны быть результаты
        }
    }
}