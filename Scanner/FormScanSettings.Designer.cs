namespace Scanner
{
    partial class FormScanSettings
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.CheckedListBox checkedListBoxVulnerabilities;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnConfirm;

        /// <summary>
        /// Освобождение ресурсов.
        /// </summary>
        /// <param name="disposing">true, если управляемые ресурсы должны быть удалены.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, сгенерированный конструктором формы

        /// <summary>
        /// Инициализация компонентов формы.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkedListBoxVulnerabilities = new System.Windows.Forms.CheckedListBox();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkedListBoxVulnerabilities
            // 
            this.checkedListBoxVulnerabilities.CheckOnClick = true;
            this.checkedListBoxVulnerabilities.FormattingEnabled = true;
            this.checkedListBoxVulnerabilities.Location = new System.Drawing.Point(12, 12);
            this.checkedListBoxVulnerabilities.Name = "checkedListBoxVulnerabilities";
            this.checkedListBoxVulnerabilities.Size = new System.Drawing.Size(1084, 340);
            this.checkedListBoxVulnerabilities.TabIndex = 0;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(12, 370);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(134, 41);
            this.btnSelectAll.TabIndex = 1;
            this.btnSelectAll.Text = "Выбрать все";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(923, 370);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(173, 41);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "Подтвердить";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // FormScanSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1109, 423);
            this.Controls.Add(this.checkedListBoxVulnerabilities);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnConfirm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormScanSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Выбор уязвимостей";
            this.ResumeLayout(false);

        }

        #endregion
    }
}
