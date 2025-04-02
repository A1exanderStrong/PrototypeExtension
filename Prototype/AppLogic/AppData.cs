using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Prototype.Entities;
using Prototype.Entities.Handbooks;

namespace Prototype
{
    public static class AppData
    {
        public static Entities.User ActiveUser { get; set; }
        public static Entities.User DefaultUser = new Entities.User
        {
            Password = ConfigurationManager.AppSettings.Get("password"),
            Login = ConfigurationManager.AppSettings.Get("login"),
            Role = new Role
            {
                Id = 0,
                Name = "По умолчанию"
            }
        };

        public static string structDump = "../../dumps/struct_dump.sql";
    }
}
