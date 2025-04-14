using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Npgsql;

namespace Scanner
{
    public partial class FormDynamic : Form
    {
        private readonly string connectionString = "Host=localhost;Port=5432;Database=scanner;Username=postgres;Password=1";
        private readonly VulnerabilityRepository repository;
        private readonly List<VulnerabilityCheck> checks;
        private List<string> selectedVulnerabilities = new List<string>();
        private List<Vulnerability> foundVulnerabilities = new List<Vulnerability>();
        private bool analysisCompleted = false;

        public FormDynamic()
        {
            InitializeComponent();

            repository = new VulnerabilityRepository(connectionString);

            // Добавляем проверки
            checks = new List<VulnerabilityCheck>
            {
                new ClassicSQLInjectionCheck(),
                new InsecureProtocolCheck(),
                new MissingSecurityHeadersCheck(),
                new BlindSQLInjectionCheck(),
                new ErrorBasedSQLInjectionCheck(),
                new UnionBasedSQLInjectionCheck(),
                new ReflectedXSSCheck(),
                new StoredXSSCheck(),
                new DOMBasedXSSCheck(),
                new CSRFMissingTokenCheck(),
                new  CSRFTokenPredictabilityCheck(),
                new InvalidSslCertificateCheck(),
                new ClickjackingCheck(),
                new ExcessiveDataExposureCheck(),
                new BrokenObjectLevelAuthorizationCheck(),
                new SSRFCheck(),
                new OpenRedirectCheck(),
                new IDORCheck(),
            };
            btnViewReport.Enabled = false;
        }

        private void TestDatabaseConnection()
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    MessageBox.Show("Успешное подключение к PostgreSQL!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnCheck_Click(object sender, EventArgs e)
        {
            if (selectedVulnerabilities.Count == 0)
            {
                MessageBox.Show("Выберите уязвимости перед сканированием!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string url = txtUrl.Text;
            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("Введите URL!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверяем, является ли введенный текст корректным URL
            Uri uriResult;
            if (!Uri.TryCreate(url, UriKind.Absolute, out uriResult) ||
                !(uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            {
                MessageBox.Show("Введите корректный URL (http:// или https://)!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            btnCheck.Enabled = false;
            btnCheck.Text = "Проверка...";
            foundVulnerabilities.Clear();
            listViewVulnerabilities.Items.Clear();
            btnViewReport.Enabled = false;

            try
            {
                listViewVulnerabilities.Items.Clear();
                bool vulnerabilityFound = false;

                progressBarAnalysis.Minimum = 0;
                progressBarAnalysis.Maximum = selectedVulnerabilities.Count;
                progressBarAnalysis.Value = 0;
                progressBarAnalysis.Visible = true;

                foreach (var check in checks)
                {
                    if (!selectedVulnerabilities.Contains(check.Name))
                        continue;

                    bool found = await check.CheckAsync(url);

                    if (found)
                    {
                        Vulnerability vulnerability = repository.GetDynamicVulnerabilityByName(check.Name);
                        if (vulnerability != null)
                        {
                            AddToListView(vulnerability);
                            vulnerabilityFound = true;
                        }
                    }

                    progressBarAnalysis.Value += 1;
                    progressBarAnalysis.Refresh();
                }

                progressBarAnalysis.Value = progressBarAnalysis.Maximum;
                btnViewReport.Enabled = foundVulnerabilities.Count > 0;

                MessageBox.Show(vulnerabilityFound ? "Проверка завершена. Уязвимости найдены!" : "Уязвимости не обнаружены.",
                                "Результат проверки", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при анализе: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCheck.Enabled = true;
                btnCheck.Text = "Проверить";
            }
        }



        private void AddToListView(Vulnerability vulnerability)
        {
            var item = new ListViewItem(vulnerability.Name);
            item.SubItems.Add(vulnerability.Severity);
            listViewVulnerabilities.Items.Add(item);
            foundVulnerabilities.Add(vulnerability);
        }
        private void btnViewReport_Click(object sender, EventArgs e)
        {
            if (foundVulnerabilities.Count > 0)
            {
                using (var reportForm = new FormReport(foundVulnerabilities))
                {
                    reportForm.ShowDialog();
                }
            }
        }

        private void btnScanSettings_Click(object sender, EventArgs e)
        {
            using (var form = new FormScanSettings(connectionString, "dynamic"))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    selectedVulnerabilities = form.SelectedVulnerabilities; // Получаем выбранные уязвимости
                }
            }
        }

    }
}
