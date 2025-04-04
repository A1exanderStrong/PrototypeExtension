using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Prototype.Entities;
using Prototype.Entities.Handbooks;

namespace Prototype.Admin
{
    public partial class AddEditUser : Form
    {
        private Entities.User selectedUser = null;
        private List<Entities.User> users = null;
        UsersForm usersForm = null; 

        /// <summary>
        /// editMode = 0; addMode = 1;
        /// </summary>
        public AddEditUser(UsersForm usersForm, int mode, List<Entities.User>users, Entities.User user = null)
        {
            InitializeComponent();
            selectedUser = user;
            this.users = users;
            this.usersForm = usersForm;
        }

        private void AddEditUser_Load(object sender, EventArgs e)
        {
            foreach (Role role in Connection.GetRoles()) 
            {
                cmbRole.Items.Add(role.Name);
            }
            if (selectedUser == null) return;
            btnAddApply.Text = "Редактировать";
            txtEmail.Text = selectedUser.Email;
            txtLogin.Text = selectedUser.Login;
            txtName.Text = selectedUser.Name;
            txtPhone.Text = selectedUser.Phone;
            cmbRole.Text = selectedUser.Role.Name;
        }

        private bool checkFields(TextBox[] textBoxes)
        {
            foreach (TextBox box in textBoxes) 
            {
                bool result = !string.IsNullOrWhiteSpace(box.Text);
                if (!result) { return result; }
            }
            return true;
        }


        private void btnAddApply_Click(object sender, EventArgs e)
        {
            if (!checkFields(new TextBox[] { txtEmail, txtLogin, txtName }))
            {
                std.error("Не все поля заполнены");
                return;
            }

            if (selectedUser == null)
            {
                foreach (var uuser in users)
                {
                    if (uuser.Login == txtLogin.Text) { std.error("Пользователь с указанным логином уже существует"); return; }
                }

                Connection.AddUser(new Entities.User
                {
                    Name = txtName.Text,
                    Email = txtEmail.Text,
                    Login = txtLogin.Text,
                    Password = std.sha256(txtPassword.Text),
                    RegistrationDate = DateTime.Now,
                    Phone = txtPhone.Text,
                    Role = Connection.GetRole(cmbRole.Text)
                });

                std.info("Пользователь успешно создан");
                usersForm.ReloadPage();
                Close();
                return;
            }

            if (txtPassword.Text.Length > 0)
            {
                var ans = std.question("Вы уверены что хотите перезаписать пароль пользователя?");
                if (ans == DialogResult.No) return;
            }
            Connection.EditUser(new Entities.User
            {
                Id = selectedUser.Id,
                Name = txtName.Text,
                Email = txtEmail.Text,
                Login = txtLogin.Text,
                Password = std.sha256(txtPassword.Text),
                Phone = txtPhone.Text,
                RegistrationDate = DateTime.Now,
                Role = Connection.GetRole(cmbRole.Text)
            });
            std.info("Пользователь успешно изменён");
            usersForm.ReloadPage();
            Close();
        }

        private void NumbersOnlyInput(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) || !char.IsDigit(e.KeyChar)) e.Handled = true;
        }
    }
}
