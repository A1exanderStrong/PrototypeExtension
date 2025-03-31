using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Prototype.Entities;
using Prototype.Entities.Handbooks;


namespace Prototype
{
    public partial class ResourceForm : Form
    {
        public ResourceForm()
        {
            InitializeComponent();
        }

        private void ResourceForm_Load(object sender, EventArgs e)
        {
            foreach (Category category in Connection.GetCategories())
                cmbCategory.Items.Add(category.Name);

            cmbCategory.SelectedIndex = 0;
        }


        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!checkFields()) { std.error("Не все поля заполнены"); return; }
            
            Resource resource = new Resource {
                Name = txtName.Text,
                Description = txtDescription.Text,
                Price = Convert.ToDouble(txtPrice.Text),
                Picture = pictureBox1.Image,
                Category = Connection.GetCategory(cmbCategory.Text),
                CreatedByUserId = AppData.ActiveUser.Id
            };

            if (Connection.HasResource(resource, "resources_requests"))
            {
                std.error("Ресурс с таким именем уже существует, придумайте другое");
                return;
            }

            Connection.SendResource(resource);
            std.info("Ресурс успешно отправлен на рассмотрение");
            Close();
        }

        private bool checkFields()
        {
            return !(string.IsNullOrWhiteSpace(txtName.Text) &&
                   string.IsNullOrWhiteSpace(txtDescription.Text) &&
                   string.IsNullOrWhiteSpace(txtPrice.Text));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "Изображения (*.png)(*.jpg)|*.png;*.jpg";

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.ImageLocation = fileDialog.FileName;
                    label1.Visible = false;
                }
            }
        }

        private void numbersOnlyInput(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
