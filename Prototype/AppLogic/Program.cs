using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Prototype
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Connection.ShowIP();

            var pwd = ConfigurationManager.AppSettings.Get("password");
            var log = ConfigurationManager.AppSettings.Get("login");

            Connection.ImportData("D:\\Makarov\\МДК 11.01\\csv\\resources2.csv", "resources");
            Application.Run(new AuthForm());
        }
    }
}
