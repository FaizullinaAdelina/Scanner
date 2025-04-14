using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Scanner
{
    public partial class MainForm: Form
    {
        public MainForm()
        {   
            InitializeComponent();
        }

        private void buttonDynamic_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormDynamic formdynamic = new FormDynamic();
            formdynamic.ShowDialog();
            this.Show();
        }

        private void buttonStatic_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormStatic formStatic = new FormStatic();
            formStatic.ShowDialog();
            this.Show();
        }

        private void buttonPortScanner_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormPortScanner formPortScanner = new FormPortScanner();
            formPortScanner.ShowDialog();
            this.Show();
        }
       

    }
}
