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
    public partial class SqlServerPicker : Form
    {
        public KeyValuePair<int, string> Pick;

        public SqlServerPicker(string title, string message, Dictionary<int,string> items)
        {
            InitializeComponent();
            Text = title;
            Message.Text = message;
            List.Items.Clear();
            List.ValueMember = "Value";
            foreach (var item in items)
            {
                List.Items.Add(item);
            }
        }

        private void SqlServerPicker_Load(object sender, EventArgs e)
        {
            List.SelectedIndex = 0;
            Pick = (KeyValuePair<int, string>)List.SelectedItem;
        }

        private void PickButton_Click(object sender, EventArgs e)
        {
            Pick = (KeyValuePair<int, string>)List.SelectedItem;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
