using MultitargetingClassLibrary;
using System;
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
            MessageBox.Show(GreetLib.Greet(this.saluteTextBox.Text));
        }
    }
}
