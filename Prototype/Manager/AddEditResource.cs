using Prototype.Entities;
using Prototype.Entities.Handbooks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prototype.Manager
{
    public partial class AddEditResource : Form
    {
        private Resource resource = null;
        private StuffForm stuffForm = null;
        private CheckResourcesForm checkForm = null;

        public AddEditResource(StuffForm stuffForm, Resource resource=null, CheckResourcesForm checkForm=null)
        {
            InitializeComponent();
            this.stuffForm = stuffForm;
            this.checkForm = checkForm;
            this.resource = resource;
        }

        private void AddEditResource_Load(object sender, EventArgs e)
        {
            foreach (Category category in Connection.GetCategories())
            {
                cmbCategory.Items.Add(category.Name);
            }
            cmbCategory.SelectedIndex = 0;

            if (resource == null) return;
            txtDescription.Text = resource.Description;
            txtName.Text = resource.Name;
            txtPrice.Text = resource.Price.ToString();
            pictureBox1.Image = resource.Picture;
            cmbCategory.Text = resource.Category.Name;
            btnAddEdit.Text = "Применить";
            btnAddImage.Text = "Изменить изображение";
        }

        private bool checkFields(TextBox[] textBoxes)
        {
            foreach (TextBox box in textBoxes)
            {
                if(string.IsNullOrWhiteSpace(box.Text)) return false;
            }
            return true;
        }

        private void btnAddEdit_Click(object sender, EventArgs e)
        {
            if (!checkFields(new TextBox[] { txtName, txtDescription, txtPrice }))
            {
                std.error("Не все поля заполнены");
                return;
            }

            var res = new Resource
            {
                Name = txtName.Text,
                Description = txtDescription.Text,
                Price = Convert.ToDouble(txtPrice.Text),
                Category = Connection.GetCategory(cmbCategory.Text),
                CreatedByUserId = AppData.ActiveUser.Id,
                Picture = pictureBox1.Image
            };

            if (resource == null)
            {
                if (checkForm == null)
                {
                    Connection.AddResource(res);
                    stuffForm.ReloadPage();
                }

                if (stuffForm == null)
                {
                    Connection.AddResourceOnRequest(res);
                    checkForm.ReloadPage();
                }
                std.info("Ресурс успешно создан");
                Close();
                return;
            }

            res.Id = resource.Id;
            if (checkForm == null)
            {
                Connection.EditResource(res);
                stuffForm.ReloadPage();
            }
            if (stuffForm == null)
            {
                Connection.EditResourceOnRequest(res);
                checkForm.ReloadPage();
            }
            std.info("Ресурс успешно изменён");
            Close();
        }

        private void numbersOnlyInput(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "Изображения (*.png)(*.jpg)|*.png;*.jpg";

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.ImageLocation = fileDialog.FileName;
                }
            }
        }
    }
}
