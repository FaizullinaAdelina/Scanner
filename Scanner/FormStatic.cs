using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Scanner.StaticChecks;

namespace Scanner
{
    public partial class FormStatic : Form
    {
        private readonly string connectionString = "Host=localhost;Port=5432;Database=scanner;Username=postgres;Password=1";
        private readonly VulnerabilityRepository repository;
        private List<string> selectedVulnerabilities = new List<string>();
        private List<Vulnerability> foundVulnerabilities = new List<Vulnerability>();

        public FormStatic()
        {
            InitializeComponent();
            repository = new VulnerabilityRepository(connectionString);
            btnViewReport.Enabled = false;
        }

        private async void btnAnalyze_Click(object sender, EventArgs e)
        {
            listViewVulnerabilities.Items.Clear();

            string code = txtCodeInput.Text;

            if (string.IsNullOrWhiteSpace(code))
            {
                var noCodeItem = new ListViewItem("Нет кода для анализа.");
                listViewVulnerabilities.Items.Add(noCodeItem);
                return;
            }
            if (selectedVulnerabilities.Count == 0)
            {
                var noCodeItem = new ListViewItem("Выберите уязвимости перед анализом.");
                listViewVulnerabilities.Items.Add(noCodeItem);
                return;
            }

            // Удаляем комментарии перед анализом
            string cleanedCode = RemoveComments(code);

            btnAnalyze.Enabled = false;
            btnAnalyze.Text = "Анализируется...";

            try
            {
                var checks = new List<StaticVulnerabilityCheck>
        {
            new EvalExecCheck(),
            new SystemCallCheck(),
            new SqlInjectionConcatCheck(),
            new WeakHashFunctionCheck(),
            new PlaintextPasswordStorageCheck(),
            new InputValidationCheck(),
            new BufferOverflowCheck(),
            new FormatStringCheck(),
            new DeprecatedLibrariesCheck(),
            new HardcodedCredentialsCheck(),
            new UnsafeMemoryFunctionsCheck(),
        };

                bool vulnerabilityFound = false;

                progressBarAnalysis.Minimum = 0;
                progressBarAnalysis.Maximum = checks.Count;
                progressBarAnalysis.Value = 0;
                progressBarAnalysis.Visible = true;

                foreach (var check in checks)
                {
                    if (!selectedVulnerabilities.Contains(check.Name))
                        continue;

                    bool found = await check.CheckAsync(cleanedCode);
                    if (found)
                    {
                        Vulnerability vulnerability = repository.GetStaticVulnerabilityByName(check.Name);
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
                btnViewReport.Enabled = foundVulnerabilities.Any();

                MessageBox.Show(vulnerabilityFound ? "Анализ завершен. Уязвимости найдены!" : "Уязвимости не обнаружены.",
                                "Результат анализа", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при анализе: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnAnalyze.Enabled = true;
                btnAnalyze.Text = "Анализировать";
            }
        }
        private string RemoveComments(string code)
        {
            // Удаление однострочных комментариев (// ...)
            code = Regex.Replace(code, @"//.*", "");

            // Удаление многострочных комментариев (/* ... */)
            code = Regex.Replace(code, @"/\*.*?\*/", "", RegexOptions.Singleline);

            return code;
        }
        private void AddToListView(Vulnerability vulnerability)
        {
            var item = new ListViewItem(vulnerability.Name);
            item.SubItems.Add(vulnerability.Severity);
            listViewVulnerabilities.Items.Add(item);
            foundVulnerabilities.Add(vulnerability);
        }

        private void btnScanSettings_Click(object sender, EventArgs e)
        {
            using (var form = new FormScanSettings(connectionString, "static"))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    selectedVulnerabilities = form.SelectedVulnerabilities;
                }
            }
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            if (foundVulnerabilities.Any())  // ✅ Используем `.Any()`
            {
                using (var reportForm = new FormReport(foundVulnerabilities))
                {
                    reportForm.ShowDialog();
                }
            }
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "C# Files (*.cs)|*.cs|All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileContent = File.ReadAllText(openFileDialog.FileName);
                    txtCodeInput.Text = fileContent;
                }
            }
        }
    }
}
