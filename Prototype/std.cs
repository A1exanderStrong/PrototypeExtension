﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prototype
{
    public static class std
    {
        private static int exitCalls = 0; 

        #region messages
        public static void error(string msg) => MessageBox.Show(msg, "ОШБИКА", MessageBoxButtons.OK, MessageBoxIcon.Error);
        public static void info(string msg) => MessageBox.Show(msg, "ИНФО", MessageBoxButtons.OK, MessageBoxIcon.Information);
        public static DialogResult question(string msg) => MessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        #endregion

        public static string sha256(string str_to_hash)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(str_to_hash));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                    builder.Append(b.ToString("x2")); // Convert to hexadecimal string

                return builder.ToString();
            }
        }

        public static void AppExit(FormClosingEventArgs e)
        {
            if (exitCalls > 0) return;
            exitCalls++;
            var answer = std.question("Вы действительно хотите выйти?");
            if (answer == DialogResult.No) e.Cancel = true;
            if (answer == DialogResult.Yes) Application.Exit();
        }

        public static async Task<Image> GetWebImageAsync(string url) => await Task.Run(() =>
        {
                    WebRequest request = WebRequest.Create(url);
                    using (var response = request.GetResponse())
                    using (var stream = response.GetResponseStream())
                        return Image.FromStream(stream);
        });

        public static Image GetWebImage(string url)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                    return Image.FromStream(stream);
            }
            catch { return null; }
        }

        public static int CountPages(int items_per_page, int items_count)
        {
            return (int)Math.Ceiling((double)items_count / items_per_page);
        }
    }
}
