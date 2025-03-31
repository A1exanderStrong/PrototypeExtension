using System;
using System.Collections.Generic;
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
    }
}
