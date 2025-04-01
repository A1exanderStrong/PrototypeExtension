using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prototype.Entities.Handbooks;

namespace Prototype.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public DateTime RegistrationDate { get; set; }

        public const byte ADMIN = 1;
        public const byte MANAGER = 2;
        public const byte USER = 3;
        public const byte DEFAULT = 0;

        public string GetRegistrationDate()
        {
            string regDate = "";
            if (RegistrationDate.Day < 10) regDate += "0";
            regDate += $"{RegistrationDate.Day}.";
            if (RegistrationDate.Month < 10) regDate += "0";
            regDate += $"{RegistrationDate.Month}.{RegistrationDate.Year}";
            return regDate;
        }
        /// <summary>
        /// Получает все ресурсы, принадлежащие этому пользователю
        /// </summary>
        public Task<List<Resource>> GetResources()
        {
            return Connection.GetResources(user_id: Id);
        }
    }
}
