using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Npgsql;

namespace Scanner
{
    public partial class FormScanSettings : Form
    {
        private readonly string connectionString;
        private readonly string analysisType; // "dynamic" или "static"
        public List<string> SelectedVulnerabilities { get; private set; } = new List<string>();

        public FormScanSettings(string connectionString, string analysisType)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.analysisType = analysisType;
            LoadVulnerabilities();
        }

        private void LoadVulnerabilities()
        {
            string tableName = analysisType == "static" ? "static_vulnerabilities" : "vulnerabilities"; // Определяем таблицу

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand($"SELECT name FROM {tableName}", connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            checkedListBoxVulnerabilities.Items.Add(reader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки уязвимостей: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxVulnerabilities.Items.Count; i++)
            {
                checkedListBoxVulnerabilities.SetItemChecked(i, true);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            SelectedVulnerabilities = checkedListBoxVulnerabilities.CheckedItems.Cast<string>().ToList();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
