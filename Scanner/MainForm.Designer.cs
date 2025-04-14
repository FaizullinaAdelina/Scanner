using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Scanner
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Button buttonDynamic;
        private System.Windows.Forms.Button buttonStatic;
        private System.Windows.Forms.Button buttonPortScanner;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.buttonDynamic = new System.Windows.Forms.Button();
            this.buttonStatic = new System.Windows.Forms.Button();
            this.buttonPortScanner = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonDynamic
            // 
            this.buttonDynamic.BackColor = System.Drawing.Color.White;
            this.buttonDynamic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonDynamic.FlatAppearance.BorderSize = 0;
            this.buttonDynamic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDynamic.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.buttonDynamic.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.buttonDynamic.Location = new System.Drawing.Point(60, 200);
            this.buttonDynamic.Name = "buttonDynamic";
            this.buttonDynamic.Size = new System.Drawing.Size(230, 80);
            this.buttonDynamic.TabIndex = 0;
            this.buttonDynamic.Text = "Динамический анализ";
            this.buttonDynamic.UseVisualStyleBackColor = false;
            this.buttonDynamic.Click += new System.EventHandler(this.buttonDynamic_Click);
            // 
            // buttonStatic
            // 
            this.buttonStatic.BackColor = System.Drawing.Color.White;
            this.buttonStatic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonStatic.FlatAppearance.BorderSize = 0;
            this.buttonStatic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStatic.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.buttonStatic.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.buttonStatic.Location = new System.Drawing.Point(310, 200);
            this.buttonStatic.Name = "buttonStatic";
            this.buttonStatic.Size = new System.Drawing.Size(230, 80);
            this.buttonStatic.TabIndex = 1;
            this.buttonStatic.Text = "Статический анализ";
            this.buttonStatic.UseVisualStyleBackColor = false;
            this.buttonStatic.Click += new System.EventHandler(this.buttonStatic_Click);
            // 
            // buttonPortScanner
            // 
            this.buttonPortScanner.BackColor = System.Drawing.Color.White;
            this.buttonPortScanner.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonPortScanner.FlatAppearance.BorderSize = 0;
            this.buttonPortScanner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPortScanner.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.buttonPortScanner.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.buttonPortScanner.Location = new System.Drawing.Point(560, 200);
            this.buttonPortScanner.Name = "buttonPortScanner";
            this.buttonPortScanner.Size = new System.Drawing.Size(230, 80);
            this.buttonPortScanner.TabIndex = 2;
            this.buttonPortScanner.Text = "Сканер портов";
            this.buttonPortScanner.UseVisualStyleBackColor = false;
            this.buttonPortScanner.Click += new System.EventHandler(this.buttonPortScanner_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 40F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(880, 520);
            this.Controls.Add(this.buttonDynamic);
            this.Controls.Add(this.buttonStatic);
            this.Controls.Add(this.buttonPortScanner);
            this.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scanner – Главная форма";
            this.ResumeLayout(false);

        }
    }
}
