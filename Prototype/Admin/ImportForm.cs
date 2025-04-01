using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Prototype.Admin
{
    public partial class ImportForm : Form
    {
        public ImportForm()
        {
            InitializeComponent();
        }

        private void txtFilepath_TextChanged(object sender, EventArgs e)
        {

        }

        private void ImportForm_Load(object sender, EventArgs e)
        {
            cmbTables.Items.AddRange(Connection.GetTables().ToArray());
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            using (var fd = new OpenFileDialog())
            {
                //"Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"'
                fd.Filter = "Значения разделённые запятой (*.csv)|*.csv";
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    txtFilepath.Text = fd.FileName;
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (!cmbTables.Items.Contains(cmbTables.Text))
            {
                std.error("Указанная таблица несуществует");
                return;
            }

            Connection.ImportData(txtFilepath.Text, cmbTables.Text);
        }
    }
}
