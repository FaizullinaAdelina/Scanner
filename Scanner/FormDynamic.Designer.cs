    using System.Windows.Forms;

    namespace Scanner
    {
        partial class FormDynamic
        {
            private System.ComponentModel.IContainer components = null;
            private TextBox txtUrl;
            private Button btnCheck;
            private ListView listViewVulnerabilities;
            private ColumnHeader columnHeaderName;
            private ColumnHeader columnHeaderSeverity;
            private ProgressBar progressBarAnalysis;

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
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.listViewVulnerabilities = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSeverity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.progressBarAnalysis = new System.Windows.Forms.ProgressBar();
            this.btnScanSettings = new System.Windows.Forms.Button();
            this.btnViewReport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(345, 20);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(500, 31);
            this.txtUrl.TabIndex = 0;
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(851, 12);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(168, 46);
            this.btnCheck.TabIndex = 1;
            this.btnCheck.Text = "Проверить";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // listViewVulnerabilities
            // 
            this.listViewVulnerabilities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderSeverity});
            this.listViewVulnerabilities.HideSelection = false;
            this.listViewVulnerabilities.Location = new System.Drawing.Point(12, 76);
            this.listViewVulnerabilities.Name = "listViewVulnerabilities";
            this.listViewVulnerabilities.Size = new System.Drawing.Size(1358, 527);
            this.listViewVulnerabilities.TabIndex = 2;
            this.listViewVulnerabilities.UseCompatibleStateImageBehavior = false;
            this.listViewVulnerabilities.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Название";
            this.columnHeaderName.Width = 600;
            // 
            // columnHeaderSeverity
            // 
            this.columnHeaderSeverity.Text = "Критичность";
            this.columnHeaderSeverity.Width = 150;
            // 
            // progressBarAnalysis
            // 
            this.progressBarAnalysis.Location = new System.Drawing.Point(12, 618);
            this.progressBarAnalysis.Name = "progressBarAnalysis";
            this.progressBarAnalysis.Size = new System.Drawing.Size(1358, 59);
            this.progressBarAnalysis.TabIndex = 3;
            // 
            // btnScanSettings
            // 
            this.btnScanSettings.Location = new System.Drawing.Point(34, 8);
            this.btnScanSettings.Name = "btnScanSettings";
            this.btnScanSettings.Size = new System.Drawing.Size(305, 55);
            this.btnScanSettings.TabIndex = 4;
            this.btnScanSettings.Text = "Параметры сканирования";
            this.btnScanSettings.UseVisualStyleBackColor = true;
            this.btnScanSettings.Click += new System.EventHandler(this.btnScanSettings_Click);
            // 
            // btnViewReport
            // 
            this.btnViewReport.Enabled = false;
            this.btnViewReport.Location = new System.Drawing.Point(1025, 12);
            this.btnViewReport.Name = "btnViewReport";
            this.btnViewReport.Size = new System.Drawing.Size(224, 46);
            this.btnViewReport.TabIndex = 5;
            this.btnViewReport.Text = "Просмотр отчета";
            this.btnViewReport.UseVisualStyleBackColor = true;
            this.btnViewReport.Click += new System.EventHandler(this.btnViewReport_Click);
            // 
            // FormDynamic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1382, 715);
            this.Controls.Add(this.btnScanSettings);
            this.Controls.Add(this.progressBarAnalysis);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.listViewVulnerabilities);
            this.Controls.Add(this.btnViewReport);
            this.Name = "FormDynamic";
            this.Text = "Scanner";
            this.ResumeLayout(false);
            this.PerformLayout();

            }

            private Button btnScanSettings;
        private Button btnViewReport;

    }
}
