using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Scanner.Tests")] // Разрешаем тестам доступ к internal-свойствам

namespace Scanner
{
    public partial class FormPortScanner : Form
    {
        public FormPortScanner()
        {
            InitializeComponent();
        }

        // FormPortScanner.cs
        private async void buttonStartScan_Click(object sender, EventArgs e)
        {
            // Блокируем кнопку Сканировать
            buttonStartScan.Enabled = false;
            buttonStartScan.Text = "Сканируется...";

            try
            {
                listBoxResults.Items.Clear();
                string ip = textBoxIP.Text;

                if (!int.TryParse(textBoxStartPort.Text, out int startPort) || !int.TryParse(textBoxEndPort.Text, out int endPort))
                {
                    MessageBox.Show("Введите корректные номера портов!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (startPort < 1 || endPort > 65535 || startPort > endPort)
                {
                    MessageBox.Show("Диапазон портов должен быть от 1 до 65535.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                progressBar.Value = 0;
                progressBar.Maximum = endPort - startPort + 1;

                var scanner = new PortScanner();

                // Выполнение сканирования в отдельной задаче
                await Task.Run(async () =>
                {
                    for (int port = startPort; port <= endPort; port++)
                    {
                        bool isOpen = await scanner.IsPortOpenAsync(ip, port);
                        string result = $"Порт {port} {(isOpen ? "открыт" : "закрыт")}";

                        Invoke(new Action(() =>
                        {
                            listBoxResults.Items.Add(result);
                            progressBar.Value++;
                        }));
                    }
                });

                MessageBox.Show("Сканирование завершено.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Разблокируем кнопку независимо от успеха или ошибки
                buttonStartScan.Enabled = true;
                buttonStartScan.Text = "Сканировать";
            }
        }
    }
}
