using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Prototype.Entities.Handbooks;

namespace Prototype
{
    public partial class AddEditHandbook : Form
    {
        private string table = "";
        private Handbook activeHandbook=null;

        /// <summary>
        /// Добавляет или редактирует в зависимости от режима запись в справочнике. 0 - режим редактирования, 1 - режим добавления
        /// </summary>
        /// <param name="handbook"></param>
        /// <param name="mode"></param>
        public AddEditHandbook(string table, Handbook hbook = null)
        {
            InitializeComponent();
            this.table = table;
            activeHandbook = hbook;
        }

        private void btnAddApply_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text)) 
            {
                std.error("Название элемента справочника не может состоять из пробелов или быть пустым"); 
                return; 
            }

            if (activeHandbook == null)
            {
                Connection.CreateHandbookItem(table, new Handbook { Name = txtName.Text });
                std.info("Запись успешно создана");
                Close();
                return;
            }

            activeHandbook.Name = txtName.Text;
            Connection.UpdateHandbookItem(table, activeHandbook);
            std.info("Запись успешно обновлена");
            Close();
        }

        private void AddEditHandbook_Load(object sender, EventArgs e)
        {
            if (activeHandbook == null) return;
            txtName.Text = activeHandbook.Name;
            btnAddApply.Text = "Применить";
        }
    }
}
