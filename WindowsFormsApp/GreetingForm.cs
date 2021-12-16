using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class GreetingForm : Form
    {
        public GreetingForm()
        {
            InitializeComponent();
        }

        private void saluteButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Hello {this.saluteTextBox.Text}");
        }
    }
}
