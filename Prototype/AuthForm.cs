using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace Prototype
{
    public partial class AuthForm : Form
    {
        // выводить количество записей на форме stuff
        bool isPasswordVisible = false;
        string captchaText = null;
        int blockDelay = 10;
        List<Control> controls = new List<Control>();
        public AuthForm()
        {
            InitializeComponent();
            togglePassword();
        }

        private void togglePassword()
        {
            txtPassword.UseSystemPasswordChar = !isPasswordVisible;
            if (!isPasswordVisible) 
            { 
                pictureBox1.ImageLocation = "../../icons/hidden.png";
            }
            else { pictureBox1.ImageLocation = "../../icons/eye.png"; }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (!checkFields()) { std.error("Не все поля заполнены!"); return; }
            if (captchaText != null && captchaText != txtCaptcha.Text)
            {
                std.error("Капча введена неверно. Система блокируется на 10 секунд");
                txtPassword.Text = "";
                BlockForm();
                await Task.Delay(1000 * blockDelay);
                UnlockForm();
                UpdateCaptcha(); 
                return;
            }
            if (txtLogin.Text == AppData.DefaultUser.Login &&
                txtPassword.Text == AppData.DefaultUser.Password) 
            {
                AppData.ActiveUser = AppData.DefaultUser;
                Hide(); 
                new MainMenu().ShowDialog(); 
                return; 
            }
            if (!Connection.Test()) { return; }

            var activeUser = Connection.Auth(txtLogin.Text, txtPassword.Text);
            if (activeUser == null) 
            { 
                std.error("Неверно указаны логин или пароль");
                txtPassword.Text = "";
                UpdateCaptcha();
                return; 
            }
            AppData.ActiveUser = activeUser;
            
            Hide();
            new MainMenu().ShowDialog();
        }

        private void UpdateCaptcha()
        {
            Size = new Size(815, 227);
            picCaptcha.Image = null;
            picCaptcha.Update();
            txtCaptcha.Text = "";

            string part0 = std.RandomString(4);
            string part1 = std.RandomString(4);
            captchaText = part0 + part1;

            // текст капчи
            Graphics g = picCaptcha.CreateGraphics();
            Font font = new Font("Comic Sans", 30);
            Point textChunk01 = new Point(std.rnd.Next(0, 50), std.rnd.Next(0, 30));
            Point textChunk02 = new Point(textChunk01.X + std.rnd.Next(25, 45), textChunk01.Y + std.rnd.Next(20, 40));
            g.DrawString(part0, font, std.RandomBrush(), textChunk01);
            g.DrawString(part1, font, std.RandomBrush(), textChunk02);

            Pen pen = new Pen(Brushes.White);

            // перечёркивающие линии
            pen.Width = 2f;
            Point initPoint01 = new Point(textChunk01.X, textChunk01.Y + std.rnd.Next(10, 40));
            Point initPoint02 = new Point(textChunk02.X, textChunk02.Y + std.rnd.Next(10, 40));
            g.DrawLine(pen, initPoint01, new Point(textChunk01.X + 90, textChunk01.Y + std.rnd.Next(10, 40)));
            g.DrawLine(pen, initPoint02, new Point(textChunk02.X + 90, textChunk02.Y + std.rnd.Next(10, 40)));

            pen.Width = 0.25f;
            // шум
            for (int x = 0; x < picCaptcha.Size.Width; x+=2)
            {
                for (int y = 0; y < picCaptcha.Size.Height; y+=2)
                {
                    if (std.rnd.Next() % 20 == 0) g.DrawRectangle(pen, new Rectangle(new Point(x, y), new Size(1,1)));
                }
            }
        }
        private void BlockForm()
        {
            controls.ForEach(control =>
            {
                control.Enabled = false;
            });
        }
        private void UnlockForm()
        {
            controls.ForEach(control =>
            {
                control.Enabled = true;
            });
        }

        private bool checkFields()
        {
            return !txtLogin.Equals(string.Empty) && !txtPassword.Equals(string.Empty);
        }

        private void AuthForm_FormClosing(object sender, FormClosingEventArgs e) => std.AppExit(e);

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;
            togglePassword();
        }

        private void btnUpdateCaptcha_Click(object sender, EventArgs e)
        {
            UpdateCaptcha();
        }

        private void AuthForm_Load(object sender, EventArgs e)
        {
            Size = new Size(427, 227);
            controls.AddRange(new List<Control> { txtCaptcha, txtLogin, txtPassword, btnUpdateCaptcha, button1 });
        }
    }
}
