namespace Scanner
{
    partial class FormStatic
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtCodeInput;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.ListView listViewVulnerabilities;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderSeverity;
        private System.Windows.Forms.Button btnScanOptions;
        private System.Windows.Forms.Button btnViewReport;
        private System.Windows.Forms.ProgressBar progressBarAnalysis;



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
            this.txtCodeInput = new System.Windows.Forms.TextBox();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.btnScanOptions = new System.Windows.Forms.Button();
            this.btnViewReport = new System.Windows.Forms.Button();
            this.listViewVulnerabilities = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSeverity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.progressBarAnalysis = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // txtCodeInput
            // 
            this.txtCodeInput.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtCodeInput.Location = new System.Drawing.Point(12, 12);
            this.txtCodeInput.Multiline = true;
            this.txtCodeInput.Name = "txtCodeInput";
            this.txtCodeInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCodeInput.Size = new System.Drawing.Size(500, 769);
            this.txtCodeInput.TabIndex = 0;
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Location = new System.Drawing.Point(752, 22);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(220, 73);
            this.btnAnalyze.TabIndex = 1;
            this.btnAnalyze.Text = "Анализировать";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Location = new System.Drawing.Point(530, 22);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(216, 73);
            this.btnLoadFile.TabIndex = 2;
            this.btnLoadFile.Text = "Загрузить файл";
            this.btnLoadFile.UseVisualStyleBackColor = true;
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // btnScanOptions
            // 
            this.btnScanOptions.Location = new System.Drawing.Point(978, 22);
            this.btnScanOptions.Name = "btnScanOptions";
            this.btnScanOptions.Size = new System.Drawing.Size(258, 73);
            this.btnScanOptions.TabIndex = 5;
            this.btnScanOptions.Text = "Параметры сканирования";
            this.btnScanOptions.UseVisualStyleBackColor = true;
            this.btnScanOptions.Click += new System.EventHandler(this.btnScanSettings_Click);
            // 
            // btnViewReport
            // 
            this.btnViewReport.Location = new System.Drawing.Point(1242, 22);
            this.btnViewReport.Name = "btnViewReport";
            this.btnViewReport.Size = new System.Drawing.Size(220, 73);
            this.btnViewReport.TabIndex = 6;
            this.btnViewReport.Text = "Просмотр отчета";
            this.btnViewReport.UseVisualStyleBackColor = true;
            this.btnViewReport.Click += new System.EventHandler(this.btnViewReport_Click);
            // 
            // listViewVulnerabilities
            // 
            this.listViewVulnerabilities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderSeverity});
            this.listViewVulnerabilities.HideSelection = false;
            this.listViewVulnerabilities.Location = new System.Drawing.Point(530, 101);
            this.listViewVulnerabilities.Name = "listViewVulnerabilities";
            this.listViewVulnerabilities.Size = new System.Drawing.Size(934, 556);
            this.listViewVulnerabilities.TabIndex = 3;
            this.listViewVulnerabilities.UseCompatibleStateImageBehavior = false;
            this.listViewVulnerabilities.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Название";
            this.columnHeaderName.Width = 320;
            // 
            // columnHeaderSeverity
            // 
            this.columnHeaderSeverity.Text = "Критичность";
            this.columnHeaderSeverity.Width = 150;
            // 
            // progressBarAnalysis
            // 
            this.progressBarAnalysis.Location = new System.Drawing.Point(530, 695);
            this.progressBarAnalysis.Name = "progressBarAnalysis";
            this.progressBarAnalysis.Size = new System.Drawing.Size(934, 59);
            this.progressBarAnalysis.TabIndex = 4;
            // 
            // FormStatic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1474, 793);
            this.Controls.Add(this.btnScanOptions);
            this.Controls.Add(this.btnViewReport);
            this.Controls.Add(this.progressBarAnalysis);
            this.Controls.Add(this.txtCodeInput);
            this.Controls.Add(this.btnAnalyze);
            this.Controls.Add(this.btnLoadFile);
            this.Controls.Add(this.listViewVulnerabilities);
            this.Name = "FormStatic";
            this.Text = "Статический анализатор";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
