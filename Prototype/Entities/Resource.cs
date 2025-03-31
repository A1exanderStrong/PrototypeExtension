using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Prototype.Entities.Handbooks;

namespace Prototype.Entities
{
    public class Resource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public Image Picture { get; set; }
        public double Price { get; set; }
        public int CreatedByUserId { get; set; } 
        public DateTime PublicationDate { get; set; }

        public User GetAuthor() => Connection.GetUser(CreatedByUserId);
    }
}
