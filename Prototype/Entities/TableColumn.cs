using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Entities
{
    public class TableColumn
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Nullable { get; set; }
        public string Key { get; set; }
        public string Extra { get; set; }

        public bool HasAutoIncrement() => Extra.Contains("auto_increment");

        public bool IsPrimaryKey() => Key == "PRI";

        public bool IsForeignKey() => Key == "MUL";
    }
}
