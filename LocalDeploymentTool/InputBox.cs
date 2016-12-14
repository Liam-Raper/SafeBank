using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalDeploymentTool
{
    public partial class InputBox : Form
    {

        public string Result;

        public InputBox(string title, string prompt)
        {
            InitializeComponent();
            Text = title;
            label1.Text = prompt;
        }

        private void InputBox_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Result = textBox1.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
