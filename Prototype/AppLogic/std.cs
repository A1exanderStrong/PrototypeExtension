using Prototype.Entities;
using Prototype.Properties;
using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using Prototype.Entities.Handbooks;

namespace Prototype
{
    public static class std
    {
        private static int exitCalls = 0;
        public static string loader_gif = "https://i.gifer.com/origin/b4/b4d657e7ef262b88eb5f7ac021edda87.gif";

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
            var answer = question("Вы действительно хотите выйти?");
            if (answer == DialogResult.No) { e.Cancel = true; exitCalls = 0; }
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

        #region auto complete logic
        public static AutoCompleteStringCollection UpdateAutoCompleteSource(List<Resource> resources)
        {
            var source = new AutoCompleteStringCollection();
            foreach (Resource resource in resources)
            {
                var suggestionBundle = "";
                foreach (string word in resource.Name.Split(' '))
                {
                    suggestionBundle += $"{word}";
                    if (source.Contains(suggestionBundle)) continue;
                    source.Add(suggestionBundle);
                    suggestionBundle += " ";
                }
                source.AddRange(resource.Name.Split(' '));
            }
            return source;
        }

        public static AutoCompleteStringCollection UpdateAutoCompleteSource(List<Entities.User> users)
        {
            var source = new AutoCompleteStringCollection();
            foreach (Entities.User user in users)
            {
                var suggestionBundle = "";
                foreach (string word in user.Name.Split(' '))
                {
                    suggestionBundle += $"{word}";
                    if (source.Contains(suggestionBundle)) continue;
                    source.Add(suggestionBundle);
                    suggestionBundle += " ";
                }
                source.AddRange(user.Name.Split(' '));
            }
            return source;
        }
        #endregion

        public static byte[] ConvertToByteArray(Image img)
        {
            try
            {
                if (img == null) return null;
                using (var ms = new MemoryStream())
                {
                    img.Save(ms, img.RawFormat);
                    return ms.ToArray();
                }
            }
            catch { return null; }
        }

        public static Image GetByteImage(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                return new Bitmap(ms);
            }
        }
    }
}
