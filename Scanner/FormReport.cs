using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Charting;
//using PdfSharp.Forms;


namespace Scanner
{
    public partial class FormReport : Form
    {
        private List<Vulnerability> vulnerabilities;
        public string ScanUrl { get; set; }
        public DateTime ScanDateTime { get; set; }

        internal FormReport(List<Vulnerability> vulnerabilities)
        {
            InitializeComponent();
            this.vulnerabilities = vulnerabilities;
            LoadReport();
        }

        private void LoadReport()
        {
            txtReport.Clear();
            txtReport.AppendText($"=== ОТЧЕТ СКАНИРОВАНИЯ ===\r\n\r\n");
            txtReport.AppendText($"Найдено уязвимостей: {vulnerabilities.Count}\r\n\r\n");
            txtReport.AppendText(new string('=', 50) + "\r\n\r\n");

            foreach (var vulnerability in vulnerabilities)
            {
                txtReport.AppendText($"► Уязвимость: {vulnerability.Name}\r\n");
                txtReport.AppendText($"├ Критичность: {vulnerability.Severity}\r\n");
                txtReport.AppendText($"├ Описание: {vulnerability.Description}\r\n");
                txtReport.AppendText($"└ Рекомендации: {vulnerability.Recommendation}\r\n\r\n");
                txtReport.AppendText(new string('-', 50) + "\r\n\r\n");
            }
        }

        private void btnExportTxt_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Текстовый файл (*.txt)|*.txt",
                Title = "Сохранить отчет как TXT"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, txtReport.Text, Encoding.UTF8);
                MessageBox.Show("Отчет сохранен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
          
        }

        // Перенос текста на новую строку
       
    }
}
