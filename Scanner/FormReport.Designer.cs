using System.Drawing;

namespace Scanner
{
    partial class FormReport
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtReport;
        private System.Windows.Forms.Button btnExportTxt;
        private System.Windows.Forms.Button btnExportPdf;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtReport = new System.Windows.Forms.TextBox();
            this.btnExportTxt = new System.Windows.Forms.Button();
            this.btnExportPdf = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtReport
            // 
            this.txtReport.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtReport.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtReport.Font = new System.Drawing.Font("Consolas", 11F);
            this.txtReport.Location = new System.Drawing.Point(0, 0);
            this.txtReport.Multiline = true;
            this.txtReport.Name = "txtReport";
            this.txtReport.ReadOnly = true;
            this.txtReport.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReport.Size = new System.Drawing.Size(1072, 539);
            this.txtReport.TabIndex = 0;
            // 
            // btnExportTxt
            // 
            this.btnExportTxt.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnExportTxt.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnExportTxt.Location = new System.Drawing.Point(528, 560);
            this.btnExportTxt.Name = "btnExportTxt";
            this.btnExportTxt.Size = new System.Drawing.Size(254, 55);
            this.btnExportTxt.TabIndex = 1;
            this.btnExportTxt.Text = "Экспорт в TXT";
            this.btnExportTxt.UseVisualStyleBackColor = false;
            this.btnExportTxt.Click += new System.EventHandler(this.btnExportTxt_Click);
            // 
            // btnExportPdf
            // 
            this.btnExportPdf.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnExportPdf.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnExportPdf.Location = new System.Drawing.Point(813, 560);
            this.btnExportPdf.Name = "btnExportPdf";
            this.btnExportPdf.Size = new System.Drawing.Size(238, 55);
            this.btnExportPdf.TabIndex = 2;
            this.btnExportPdf.Text = "Экспорт в PDF";
            this.btnExportPdf.UseVisualStyleBackColor = false;
            this.btnExportPdf.Click += new System.EventHandler(this.btnExportPdf_Click);
            // 
            // FormReport
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1072, 641);
            this.Controls.Add(this.txtReport);
            this.Controls.Add(this.btnExportTxt);
            this.Controls.Add(this.btnExportPdf);
            this.Name = "FormReport";
            this.Text = "Отчет об уязвимостях";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
    