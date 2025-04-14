namespace Scanner
{
    partial class FormPortScanner
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                if (disposing && (components != null)) components.Dispose();
                base.Dispose(disposing);
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelIP = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.labelStartPort = new System.Windows.Forms.Label();
            this.textBoxStartPort = new System.Windows.Forms.TextBox();
            this.labelEndPort = new System.Windows.Forms.Label();
            this.textBoxEndPort = new System.Windows.Forms.TextBox();
            this.buttonStartScan = new System.Windows.Forms.Button();
            this.listBoxResults = new System.Windows.Forms.ListBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // labelIP
            // 
            this.labelIP.AutoSize = true;
            this.labelIP.Location = new System.Drawing.Point(20, 20);
            this.labelIP.Name = "labelIP";
            this.labelIP.Size = new System.Drawing.Size(103, 25);
            this.labelIP.TabIndex = 0;
            this.labelIP.Text = "IP-адрес:";
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(150, 20);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(200, 31);
            this.textBoxIP.TabIndex = 1;
            // 
            // labelStartPort
            // 
            this.labelStartPort.AutoSize = true;
            this.labelStartPort.Location = new System.Drawing.Point(20, 70);
            this.labelStartPort.Name = "labelStartPort";
            this.labelStartPort.Size = new System.Drawing.Size(182, 25);
            this.labelStartPort.TabIndex = 2;
            this.labelStartPort.Text = "Начальный порт:";
            // 
            // textBoxStartPort
            // 
            this.textBoxStartPort.Location = new System.Drawing.Point(200, 70);
            this.textBoxStartPort.Name = "textBoxStartPort";
            this.textBoxStartPort.Size = new System.Drawing.Size(150, 31);
            this.textBoxStartPort.TabIndex = 3;
            // 
            // labelEndPort
            // 
            this.labelEndPort.AutoSize = true;
            this.labelEndPort.Location = new System.Drawing.Point(20, 120);
            this.labelEndPort.Name = "labelEndPort";
            this.labelEndPort.Size = new System.Drawing.Size(170, 25);
            this.labelEndPort.TabIndex = 4;
            this.labelEndPort.Text = "Конечный порт:";
            // 
            // textBoxEndPort
            // 
            this.textBoxEndPort.Location = new System.Drawing.Point(200, 120);
            this.textBoxEndPort.Name = "textBoxEndPort";
            this.textBoxEndPort.Size = new System.Drawing.Size(150, 31);
            this.textBoxEndPort.TabIndex = 5;
            // 
            // buttonStartScan
            // 
            this.buttonStartScan.Location = new System.Drawing.Point(150, 180);
            this.buttonStartScan.Name = "buttonStartScan";
            this.buttonStartScan.Size = new System.Drawing.Size(200, 50);
            this.buttonStartScan.TabIndex = 6;
            this.buttonStartScan.Text = "Сканировать";
            this.buttonStartScan.UseVisualStyleBackColor = true;
            this.buttonStartScan.Click += new System.EventHandler(this.buttonStartScan_Click);
            // 
            // listBoxResults
            // 
            this.listBoxResults.FormattingEnabled = true;
            this.listBoxResults.ItemHeight = 25;
            this.listBoxResults.Location = new System.Drawing.Point(400, 20);
            this.listBoxResults.Name = "listBoxResults";
            this.listBoxResults.Size = new System.Drawing.Size(400, 379);
            this.listBoxResults.TabIndex = 7;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(20, 250);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(350, 30);
            this.progressBar.TabIndex = 8;
            // 
            // FormPortScanner
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(850, 450);
            this.Controls.Add(this.labelIP);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.labelStartPort);
            this.Controls.Add(this.textBoxStartPort);
            this.Controls.Add(this.labelEndPort);
            this.Controls.Add(this.textBoxEndPort);
            this.Controls.Add(this.buttonStartScan);
            this.Controls.Add(this.listBoxResults);
            this.Controls.Add(this.progressBar);
            this.Name = "FormPortScanner";
            this.Text = "Сканер портов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label labelIP;
        internal System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Label labelStartPort;
        internal System.Windows.Forms.TextBox textBoxStartPort;
        private System.Windows.Forms.Label labelEndPort;
        internal System.Windows.Forms.TextBox textBoxEndPort;
        internal System.Windows.Forms.Button buttonStartScan;
        internal System.Windows.Forms.ListBox listBoxResults;
        private System.Windows.Forms.ProgressBar progressBar;
    }

    #endregion
}