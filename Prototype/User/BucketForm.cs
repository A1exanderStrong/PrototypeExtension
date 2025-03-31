using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Prototype.Entities;

namespace Prototype.User
{
    public partial class BucketForm : Form
    {
        public List<Resource> resources = new List<Resource>();
        private static int rowsHeight = 70;
        StuffForm stuffForm;

        public BucketForm(List<Resource> resources, StuffForm stuffForm)
        {
            InitializeComponent();
            this.resources = resources;
            this.stuffForm = stuffForm;
        }

        private void BucketForm_Load(object sender, EventArgs e)
        { 
            createColumns();
            updateRows();
        }

        private void createColumns()
        {
            var name = new DataGridViewTextBoxColumn
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells,
                HeaderText = "Наименование",
            };
            var price = new DataGridViewTextBoxColumn
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells,
                HeaderText = "Цена"
            };
            var picture = new DataGridViewImageColumn
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                HeaderText = "Изображение",
                Width = 100,
                ImageLayout = DataGridViewImageCellLayout.Zoom
            };

            dgv.Columns.Add(picture);
            dgv.Columns.Add(name);
            dgv.Columns.Add(price);
        }

        public void updateRows()
        {
            dgv.Rows.Clear();
            dgv.RowTemplate.Height = rowsHeight;
            double price = 0;
            foreach (Resource res in resources)
            {
                dgv.Rows.Add(res.Picture, res.Name, res.Price);
                price += res.Price;
            }
            totalPrice.Text = $"Итого: {price}";
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0) return;

            string selectedName = dgv.SelectedRows[0].Cells[1].Value.ToString();
            foreach (Resource res in resources)
            {
                if (res.Name == selectedName) 
                { 
                    resources.Remove(res);
                    stuffForm.bucket.Remove(res); 
                    updateRows(); 
                    return;  
                }
            }
        }

        private void btnClearBucket_Click(object sender, EventArgs e)
        {
            resources.Clear();
            stuffForm.bucket.Clear();
            updateRows();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            if (resources.Count == 0)
            {
                std.error("Добавьте товары в корзину");
                return;
            }

            Connection.MakePurchase(AppData.ActiveUser, resources);
            std.info("Оплата проведена успешно");
            stuffForm.bucket.Clear();
            Close();
        }
    }
}
