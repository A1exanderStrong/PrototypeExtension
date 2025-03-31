using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using System.Xml.Linq;
using Prototype.Entities;
using Prototype.Entities.Handbooks;
using Prototype.Manager;
using Prototype.Properties;

namespace Prototype
{
    public partial class CheckResourcesForm : Form
    {
        private List<Resource> resources = new List<Resource>();
        private static int rowsHeight = 70;

        #region search params
        private static string name = string.Empty;
        private static string filter = string.Empty;
        private static string sort = string.Empty;
        private static bool sort_reversed = false;
        #endregion

        private static int maxRequests = 20;

        public CheckResourcesForm()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            new MainMenu().ShowDialog();
        }

        private void createColumns()
        {
            // picture | name | category | price | author

            var name = new DataGridViewTextBoxColumn
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells,
                HeaderText = "Наименование",
            };
            var category = new DataGridViewTextBoxColumn
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                HeaderText = "Категория"
            };
            var price = new DataGridViewTextBoxColumn
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells,
                HeaderText = "Цена"
            };
            var picture = new DataGridViewImageColumn
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader,
                HeaderText = "Изображение",
                Width = 100,
                ImageLayout = DataGridViewImageCellLayout.Zoom
            };
            var author = new DataGridViewTextBoxColumn
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells,
                HeaderText = "Автор"
            };

            dgv.Columns.Add(picture);
            dgv.Columns.Add(name);
            dgv.Columns.Add(category);
            dgv.Columns.Add(price);
            dgv.Columns.Add(author);
        }

        private void updateRows()
        {
            dgv.Rows.Clear();
            dgv.RowTemplate.Height = rowsHeight;

            // picture | name | category | price | author
            
            foreach (var resource in resources)
            {
                dgv.Rows.Add(resource.Picture,
                    resource.Name,
                    resource.Category.Name,
                    resource.Price,
                    Connection.GetUser(resource.CreatedByUserId).Login);
            }
        }

        public async void ReloadPage()
        {
            loaderImage.Visible = true;
            labelResourcesNotFound.Visible = false;
            var request = Connection.GetResourcesOnRequest();
            await request;
            for (int i = 0; i < maxRequests; i++)
            {
                //request.Wait((int)(1000 * resources_update_delay));
                if (request.IsCompleted)
                {
                    resources = request.Result;
                    updateRows();
                    loaderImage.Visible = false;
                    return;
                }
                //await Task.Delay((int)(1000 * resources_update_delay));
            }
            loaderImage.Visible = false;
            labelResourcesNotFound.Visible = true;
        }

        private void CheckResourcesForm_FormClosing(object sender, FormClosingEventArgs e) => std.AppExit(e);

        private void CheckResourcesForm_Load(object sender, EventArgs e)
        {
            createColumns();

            // MB
            //categories = Connection.GetCategories();
            //foreach (Category category in categories)
            //{
            //    comboBox1.Items.Add(category.Name);
            //}

            ReloadPage();
        }

        private void buttonAcceptResource_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0) return;
            Resource selected_resource = Connection.GetResourceOnRequest(dgv.SelectedRows[0].Cells[1].Value.ToString());
            if (selected_resource == null) return;

            if (Connection.HasResource(selected_resource, "resources"))
            {
                std.error("Ресурс с таким именем уже существует, придумайте другое");
                return;
            }

            Connection.AcceptResource(selected_resource);
            ReloadPage();
            std.info("Ресурс успешно добавлен");
        }

        private void btnDenyResource_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0) return;
            Resource selected_resource = Connection.GetResourceOnRequest(dgv.SelectedRows[0].Cells[1].Value.ToString());
            if (selected_resource == null) return;

            Connection.DenyResource(selected_resource);
            ReloadPage();
            std.info("Запрос успшено отклонён");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0) return;
            Resource resource = Connection.GetResourceOnRequest(dgv.SelectedRows[0].Cells[1].Value.ToString());
            if (resource == null) return;

            new AddEditResource(null, resource, this).ShowDialog();
        }
    }
}
