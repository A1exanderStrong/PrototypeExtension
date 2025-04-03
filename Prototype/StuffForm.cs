using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Prototype.Admin;
using Prototype.Entities;
using Prototype.Entities.Handbooks;
using Prototype.Manager;

namespace Prototype
{
    public partial class StuffForm : Form
    {
        private const int rowsHeight = 100;
        private const int chunckSize = 20; // количество отображаемых ресурсов
        private int limit = chunckSize;
        private int offset = 0;
        private int itemsDisplayed = 1;
        private int pageNum = 1;
        private int itemsCount = Connection.GetRecordsCount("resources");
        List<LinkLabel> pagesClickable = new List<LinkLabel>();
        //private readonly static long resourcesCount = Connection.GetRecordsCount("resources");
        //private static int totalPages = std.CountPages(chunckSize, (int)resourcesCount);


        private List<Resource> resources = new List<Resource>();
        private List<Resource> owningResources = new List<Resource>();
        public List<Resource> bucket = new List<Resource>();

        User.BucketForm bucketForm;
        bool isRequestFinished = false;
        #region search params
        private string name = "";
        private string filter = "";
        private string sort = "";
        private bool sort_reversed = false;
        #endregion

        private readonly float resources_update_delay = 5f; // в секундах
        private const int maxRequests = 20; // Максимальное количество попыток обновления списка ресурсов

        public StuffForm()
        {
            InitializeComponent();
            loaderImage.ImageLocation = std.loader_gif;
            progressBar1.Maximum = chunckSize;
            progressBar1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            
            new MainMenu().ShowDialog();
        }

        private void setUserAbilities()
        {
            if (AppData.ActiveUser.Role.Id == Entities.User.ADMIN)
            {
                btnBucket.Visible = false;
                btnAddToBucket.Visible = false;
                btnEditResource.Visible = false;
                btnRemoveResource.Visible = false;
            }
            if (AppData.ActiveUser.Role.Id == Entities.User.MANAGER)
            {
                btnBucket.Visible = false;
                btnAddToBucket.Text = "Добавить"; //688; 596 --- 525; 596
                //btnEditResource.Location = new Point(688, 596);
                //btnRemoveResource.Location = new Point(525, 596);
            }
            if (AppData.ActiveUser.Role.Id == Entities.User.USER)
            {
                btnEditResource.Visible = false;
                btnRemoveResource.Visible = false;
            }
        }

        private void StuffForm_Load(object sender, EventArgs e)
        {
            createColumns();

            //categories = Connection.GetCategories();
            ComboBoxCategories.Items.Add("Все ресурсы");
            ComboBoxCategories.SelectedIndex = 0;
            comboBoxSort.SelectedIndex = 0;

            setUserAbilities();
            foreach(var category in Connection.GetCategories())
                ComboBoxCategories.Items.Add(category.Name);

            ReloadPage();
        }

        private void PageNumberClick(object sender, EventArgs e)
        {
            var label = (LinkLabel)sender;
            string num = label.Text.Trim(',');
            pageNum = Convert.ToInt32(num);
            limit = pageNum*chunckSize;
            offset = (pageNum - 1) * chunckSize;
            ReloadPage();
        }

        private void updatePagesCount()
        {
            pagesClickable.ForEach(label => {
                Controls.Remove(label);
            });
            pagesClickable.Clear();

            SetPage(1);
            itemsCount = Connection.GetRecordsCount("resources");
            if (filter != "") itemsCount = resources.Count;

            System.Drawing.Point initLocation = new System.Drawing.Point(486, 627);
            
            for (int i = 0; i < std.CountPages(chunckSize, itemsCount); i++)
            {
                pagesClickable.Add(new LinkLabel
                {
                    Text = $"{i + 1}",
                    AutoSize = true,
                    Location = new System.Drawing.Point(initLocation.X + (20 * i + 1), initLocation.Y)
                });
            }
            pagesClickable.ForEach(label =>
            {
                label.Parent = this;
                label.Show();
                label.Click += PageNumberClick;
            });
        }

        public async void ReloadPage()
        {
            resources.Clear();
            labelResourcesNotFound.Visible = false;
            loaderImage.Visible = true;
            #region threading
            //isRequestFinished = false;
            //Thread resourcesThread = new Thread(new ThreadStart(resource_thread));
            //resourcesThread.Start();
            //while (!isRequestFinished)
            //{
            //    loaderImage.Visible = true;
            //    await Task.Delay((int)(resources_update_delay * 1000));
            //}
            //resourcesThread.Abort();
            //updateRows();
            //loaderImage.Visible = false;
            //labelResourcesNotFound.Visible = false;
            #endregion

            var getResources = Connection.GetResources(name, filter, sort, limit, offset, sort_reversed);
            var getOwning = AppData.ActiveUser.GetResources();
            await getResources;
            await getOwning;
            
            for (int i = 0; i < maxRequests; i++)
            {
                //request.Wait((int)(10000 * resources_update_delay));
                if (getResources.IsCompleted && getOwning.IsCompleted)
                {
                    resources = getResources.Result;
                    owningResources = getOwning.Result;
                    updateRows();
                    updatePagesCount();
                    loaderImage.Visible = false;
                    return;
                }
                //await Task.Delay((int)(1000 * resources_update_delay));
            }
            loaderImage.Visible = false;
            labelResourcesNotFound.Visible = true;
        }

        #region threading
        private async void resource_thread()
        {
            var request = Connection.GetResources(name, filter, sort, chunckSize, offset, sort_reversed);
            await request;
            for (int i = 0; i < maxRequests; i++)
            {
                //request.Wait((int)(1000 * resources_update_delay));
                if (request.IsCompleted)
                {
                    resources = request.Result;
                    isRequestFinished = request.IsCompleted;
                    return;
                }
                //wait Task.Delay((int)(1000 * resources_update_delay));
            }
        } 
        #endregion

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
            itemsDisplayed = 0;
            lbPageNum.Text = $"Номер страницы: {pageNum}";
            // picture | name | category | price | author
            foreach (Resource resource in resources) 
            {
                dgv.Rows.Add(resource.Picture,
                            resource.Name,
                            resource.Category.Name,
                            resource.Price,
                            resource.GetAuthor().Login);
                itemsDisplayed++;
            }
            lbDisplayed.Text = $"Отображено ресурсов: {itemsDisplayed}";
            txtResourceName.AutoCompleteCustomSource = std.UpdateAutoCompleteSource(resources);
        }

        private void txtResourceName_TextChanged(object sender, EventArgs e)
        {
            name = txtResourceName.Text;
            if (name.Length >= 3 || name.Length == 0) ReloadPage();
        }

        private void StuffForm_FormClosing(object sender, FormClosingEventArgs e) => std.AppExit(e);

        private void ComboBoxCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxCategories.SelectedIndex < 0) return;
            if (ComboBoxCategories.SelectedIndex == 0) { filter = ""; ReloadPage(); return; }

            foreach (var category in Connection.GetCategories())
            {
                if (category.Name == ComboBoxCategories.SelectedItem.ToString())
                { 
                    filter = category.Id.ToString();
                    break;
                }
            }
            ReloadPage();
        }

        private void comboBoxSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBoxSort.SelectedIndex;
            if (index == 0) { sort = ""; ReloadPage(); return; }
            if (index == 1 || index == 2) sort = "publication_date";
            if (index == 3 || index == 4) sort = "price";
            sort_reversed = index == 1 || index == 3;
            ReloadPage();
        }

        private void buttonAddToBucket_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0) return;
            if (AppData.ActiveUser.Role.Id == Entities.User.MANAGER)
            {
                new AddEditResource(this).ShowDialog();
                return;
            }

            var selectedRow = dgv.SelectedRows[0].Cells[1].Value.ToString();
            Resource selectedResource = Connection.GetResource(selectedRow);
            if (selectedResource == null) return;
            foreach (Resource resource in bucket)
                if (resource.Id == selectedResource.Id) return;

            foreach (Resource resource in owningResources)
                if (resource.Id == selectedResource.Id) { std.error("Вы уже покупали этот ресурс"); return; }

            bucket.Add(selectedResource);
            std.info("Товар добавлен в корзину");
            if (bucketForm == null) return;
            bucketForm.updateRows();
        }

        private void btnBucket_Click(object sender, EventArgs e)
        {
            bucketForm = new User.BucketForm(bucket, this);
            bucketForm.ShowDialog();
        }

        private void btnRemoveResource_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0) return;
            var selectedRow = dgv.SelectedRows[0].Cells[1].Value.ToString();
            Resource selectedResource = Connection.GetResource(selectedRow);

            if (selectedResource == null) return;
            var answer = std.question($"Вы действительно хотите удалить ресурс \"{selectedResource.Name}\"?");
            if (answer == DialogResult.No) return; 
            Connection.RemoveResource(selectedResource);
            std.info("Ресурс успешно удалён");
            ReloadPage();
        }

        private void btnEditResource_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0) return;
            Resource resource = Connection.GetResource(dgv.SelectedRows[0].Cells[1].Value.ToString());
            if (resource == null) return;

            new AddEditResource(this, resource).ShowDialog();
        }

        private void SetPage(int page_number)
        {
            pageNum = page_number;
            limit = pageNum * chunckSize;
            offset = (pageNum - 1) * chunckSize;
        }

        private void btnIncPage_Click(object sender, EventArgs e)
        {
            //int itemsCount = Connection.GetRecordsCount("resources");
            int pageCount = std.CountPages(chunckSize, itemsCount);
            if (pageNum + 1 > pageCount) return;
            SetPage(pageNum++);
            ReloadPage();
        }

        private void btnDecPage_Click(object sender, EventArgs e)
        {
            if (pageNum - 1 <= 0) return;
            SetPage(pageNum--);
            ReloadPage();
        }
    }
}
