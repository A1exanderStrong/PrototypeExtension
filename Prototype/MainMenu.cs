using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Prototype.Entities;

namespace Prototype
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            new AuthForm().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            new StuffForm().ShowDialog();
        }

        private void buttonSendResource_Click(object sender, EventArgs e)
        {
            new ResourceForm().ShowDialog();
        }

        private void buttonResourcesOnRequest_Click(object sender, EventArgs e)
        {
            Hide();
            new CheckResourcesForm().ShowDialog();
        }

        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            std.AppExit(e);
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            switch (AppData.ActiveUser.Role.Id) 
            {
                case Entities.User.ADMIN:
                    {
                        btnSendResource.Visible = false;
                        btnResourcesOnRequest.Visible = false;
                        btnSalesReport.Visible = false;
                        btnResourcesOwned.Visible = false;
                        btnHandbook.Visible = false;
                        break;
                    }
                case Entities.User.MANAGER:
                    {
                        btnRecoverStruct.Visible = false;
                        btnImport.Visible = false;
                        buttonUsers.Visible = false;
                        btnSendResource.Visible = false;
                        btnResourcesOwned.Visible = false;
                        break;
                    }
                case Entities.User.USER:
                    {
                        btnRecoverStruct.Visible = false;
                        btnImport.Visible = false;
                        buttonUsers.Visible = false;
                        btnSalesReport.Visible = false;
                        btnResourcesOnRequest.Visible = false;
                        break;
                    }
                case Entities.User.DEFAULT:
                    {
                        btnSendResource.Visible = false;
                        btnResourcesOnRequest.Visible = false;
                        btnResourcesOwned.Visible = false;
                        buttonUsers.Visible = false;
                        btnSalesReport.Visible = false;
                        btnResources.Visible = false;
                        break;
                    }
            }
            resizeForm(AppData.ActiveUser.Role.Id);
        }

        private void resizeForm(int role)
        {
            // first button pos - 78, 73
            // second button pos - 78, 116
            // third button pos - 78, 159

            // form size +60 for each button
            // btnBack pos = form size - 90

            Point LocFirstBtn = new Point(78, 73);
            Point LocSecondBtn = new Point(78, 116);
            Point LocThirdBtn = new Point(78, 159);

            if (role == Entities.User.ADMIN)
            {
                buttonUsers.Location = LocSecondBtn;
                btnRecoverStruct.Location = LocThirdBtn;
                btnImport.Location = new Point(78, 202);
                btnBack.Location = new Point(78, 310);
                Size = new Size(423, 400);
            }
            if (role == Entities.User.MANAGER)
            {
                btnResourcesOnRequest.Location = LocThirdBtn;
                btnHandbook.Location = new Point(78, 202);
                btnBack.Location = new Point(78, 310);
                Size = new Size(423, 400);
            }
            if (role == Entities.User.USER)
            {
                btnSendResource.Location = LocSecondBtn;
                btnResourcesOwned.Location = LocThirdBtn;
                btnBack.Location = new Point(78, 250);
                Size = new Size(423, 340);
            }
            if (role == Entities.User.DEFAULT)
            {
                btnImport.Location = LocFirstBtn;
                btnRecoverStruct.Location = LocSecondBtn;
                btnBack.Location = new Point(78, 190);
                Size = new Size(423, 280);
            }
        }

        private void buttonUsers_Click(object sender, EventArgs e)
        {
            Hide();
            new UsersForm().ShowDialog();
        }

        private void btnHandbook_Click(object sender, EventArgs e)
        {
            Hide();
            new HandbooksForm().ShowDialog();
        }

        private void btnResourcesOwned_Click(object sender, EventArgs e)
        {
            Hide();
            new OwnedResourcesForm().ShowDialog();
        }
    }
}
