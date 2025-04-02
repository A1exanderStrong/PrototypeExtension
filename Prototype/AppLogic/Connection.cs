using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using MySql.Data.MySqlClient;
using Prototype.Entities;
using Prototype.Properties;
using Prototype.Entities.Handbooks;
using System.Linq.Expressions;
using System.CodeDom;
using System.Xml.Linq;
using System.Management.Instrumentation;
using System.Xml.Schema;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace Prototype
{
    public static class Connection
    {
        #region connection params
        // ------------------------ IFNINITY FREE ------------------------ 
        //public const string host = "sql109.infinityfree.com";
        //public const string database = "if0_38430818_prototype";
        //public const string usr = "if0_38430818";
        //public const string pwd = "mqiFkE3p5xnhgb";

        // ------------------------ FREEDB TECH ------------------------ 
        //public const string host = "sql.freedb.tech";
        //public const string database = "freedb_prototype";
        //public const string usr = "freedb_main-user";
        //public const string pwd = "c?D#mUsCTyFR2y4";

        // ------------------------ MY ARENA ------------------------
        //public const string host = "46.174.50.9";
        //public const string host = "db4.myarena.ru";
        //public const string database = "u40702_prototype";
        //public const string usr = "u40702_master";
        //public const string pwd = "DfgJ8934S";

        // ------------------------ LOCAL ------------------------
        public const string host = "localhost";
        public const string database = "prototype";
        public const string usr = "root";
        public const string pwd = "root";

        public static string conString = $"host={host};uid={usr};pwd={pwd};database={database};";
        public static string conStringNoDB = $"host={host};uid={usr};pwd={pwd};";
        #endregion

        public static void ShowIP()
        {
            var request = WebRequest.Create("http://ipinfo.io/ip");
            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
                std.info(reader.ReadToEnd());
        }

        /// <summary>
        /// Проверяет возможность подключения к БД
        /// </summary>
        public static bool Test()
        {
            try
            {
                using (var con = new MySqlConnection(conString))
                {
                    con.Open();
                }
                return true;
            } // System.TimeoutException
            catch (TimeoutException)
            {
                std.error("Время подключения истекло, проверьте ваше подключение к интернету");
                return false;
            }
            catch {
                throw; }
            //return false; }
        }

        #region users
        /// <summary>
        /// Проверяет наличие пользователя в БД
        /// </summary>
        public static Entities.User Auth(string login, string password)
        {
            string sql = "SELECT * FROM `users` WHERE login=@Login AND password=@Password;";
            var user = new Entities.User();

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand(sql, con);
                try
                {
                    password = std.sha256(password);
                    cmd.Parameters.AddWithValue("@Login", login);
                    cmd.Parameters.AddWithValue("@Password", password);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows) { return null; }

                        reader.Read();
                        user.Id = reader.GetInt32("id");
                        user.Login = reader.GetString("login");
                        user.Password = reader.GetString("password");
                        user.Email = reader.GetString("email");
                        user.Name = reader.GetString("name");
                        user.Role = GetRole(reader.GetInt32("role"));
                        user.RegistrationDate = reader.GetDateTime("registration_date");
                    }
                }
                catch
                {
                    return null;
                }
            }
            return user;
        }
        public static Entities.User GetUser(int id)
        {
            using (var con = new MySqlConnection(conString))
            {
                con.Open();
                string sql = $"SELECT * FROM `users` WHERE id={id}";
                var cmd = new MySqlCommand(sql, con);
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.HasRows) return null;

                    reader.Read();
                    return new Entities.User
                    {
                        Id = reader.GetInt32("id"),
                        Login = reader.GetString("login"),
                        Password = reader.GetString("password"),
                        Email = reader.GetString("email"),
                        Name = reader.GetString("name"),
                        Role = GetRole(reader.GetInt32("role")),
                        RegistrationDate = reader.GetDateTime("registration_date")
                    };
                }
            }
        }
        public static Entities.User GetUser(string login)
        {
            using (var con = new MySqlConnection(conString))
            {
                con.Open();
                string sql = $"SELECT * FROM `users` WHERE login='{login}'";
                var cmd = new MySqlCommand(sql, con);
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.HasRows) return null;

                    reader.Read();
                    return new Entities.User
                    {
                        Id = reader.GetInt32("id"),
                        Login = reader.GetString("login"),
                        Password = reader.GetString("password"),
                        Email = reader.GetString("email"),
                        Name = reader.GetString("name"),
                        Role = GetRole(reader.GetInt32("role")),
                        RegistrationDate = reader.GetDateTime("registration_date")
                    };
                }
            }
        }
        public static void RemoveUser(Entities.User user)
        {
            using (var con = new MySqlConnection(conString))
            {
                con.Open();

                string sql = $"DELETE FROM `users` WHERE id='{user.Id}'";

                var cmd = new MySqlCommand(sql, con);
                cmd.ExecuteNonQuery();
            }
        }
        public static void EditUser(Entities.User user)
        {
            using (var con = new MySqlConnection(conString))
            {
                con.Open();

                string sql = "UPDATE `users` SET " +
                    "login=@Login, " +
                    "password=@Password, " +
                    "email=@Email, " +
                    "registration_date=@RegistrationDate, " +
                    "role=@Role, " +
                    "name=@Name " +
                    $"WHERE id={user.Id}";

                var cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("Login", user.Login);
                cmd.Parameters.AddWithValue("Password", user.Password);
                cmd.Parameters.AddWithValue("Email", user.Email);
                cmd.Parameters.AddWithValue("RegistrationDate", user.RegistrationDate);
                cmd.Parameters.AddWithValue("Role", user.Role.Id);
                cmd.Parameters.AddWithValue("Name", user.Name);
                cmd.CommandText = sql;

                cmd.ExecuteNonQuery();
            }
        }
        public static void AddUser(Entities.User user)
        {
            using (var con = new MySqlConnection(conString))
            {
                con.Open();

                string sql = "INSERT INTO `users`(login, password, email, registration_date, role, name) " +
                    "VALUES(@Login, @Password, @Email, @RegistrationDate, @Role, @Name)";

                var cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("Login", user.Login);
                cmd.Parameters.AddWithValue("Password", user.Password);
                cmd.Parameters.AddWithValue("Email", user.Email);
                cmd.Parameters.AddWithValue("RegistrationDate", user.RegistrationDate);
                cmd.Parameters.AddWithValue("Role", user.Role.Id);
                cmd.Parameters.AddWithValue("Name", user.Name);
                cmd.CommandText = sql;

                cmd.ExecuteNonQuery();
            }
        }
        public async static Task<List<Entities.User>> GetUsers(string name = "", int role = 0, string sort = "", int limit = -1, int offset = -1, bool sort_reverse = false)
        {
            string select = $"SELECT * FROM `users`";
            string _search = $"WHERE `login` LIKE '%{name}%'";
            string _role01 = $"AND `role`={role}";
            string _role02 = $"WHERE `role`={role}";
            string _sort = $"ORDER BY {sort}";
            string _limit = $"LIMIT {limit}";
            string _offset = $"OFFSET {offset}";

            string sql = select;

            bool name_not_empty = !name.Equals(string.Empty);
            bool sort_not_empty = !sort.Equals(string.Empty);

            if (name_not_empty) sql += $" {_search}";

            if (name_not_empty && role != 0) sql += $" {_role01}";
            else if (role != 0) sql += $" {_role02}";

            if (sort_not_empty && sort_reverse) sql += $" {_sort} DESC";
            else if (sort_not_empty) sql += $" {_sort}";

            if (limit != -1) sql += $" {_limit}";
            if (offset != -1) sql += $" {_offset}";

            var users = new List<Entities.User>();
            using (var con = new MySqlConnection(conString))
            {
                con.Open();

                var cmd = new MySqlCommand(sql, con);
                await Task.Run(() =>
                {
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            users.Add(new Entities.User
                            {
                                Id = reader.GetInt32("id"),
                                Login = reader.GetString("login"),
                                Email = reader.GetString("email"),
                                Name = reader.GetString("name"),
                                Role = GetRole(reader.GetInt32("role")),
                                RegistrationDate = reader.GetDateTime("registration_date")
                            });
                        }
                });
            }
            return users;
        } 
        #endregion

        #region resources
        public async static Task<List<Resource>> GetResources(string name = "", string category = "", string sort = "", int limit = -1, int offset = -1, bool reverse_sort = false, int user_id = -1)
        {
            string select = $"SELECT * FROM `resources`";
            // выбираем все ресурсы из purchases в которых id пользователя равен указанному в параметре.
            string select02 = $"SELECT * FROM `resources`, purchases " +
                               $"WHERE `purchases`.`user_id`={user_id} AND " +
                               $"`purchases`.`resource_id`=`resources`.`id`";
            string _search = $"AND `resources`.`name` LIKE '%{name}%'";
            string _search02 = $"WHERE `resources`.`name` LIKE '%{name}%'";
            string _category01 = $"AND `category`={category}";
            string _category02 = $"WHERE `category`={category}";
            string _sort = $"ORDER BY {sort}";
            string _limit = $"LIMIT {limit}";
            string _offset = $"OFFSET {offset}";

            string sql = "";

            if (user_id != -1) sql = select02;
            if (user_id == -1) sql = select;

            bool name_not_empty = !name.Equals(string.Empty);
            bool category_not_empty = !category.Equals(string.Empty);
            bool sort_not_empty = !sort.Equals(string.Empty);
            bool uid_filter_active = user_id != -1;

            if (name_not_empty)
            {
                if (uid_filter_active) sql += $" {_search}";
                if (!uid_filter_active) sql += $" {_search02}";
            }

            if (category_not_empty)
            {
                if (name_not_empty || uid_filter_active) sql += $" {_category01}";
                else sql += $" {_category02}";
            }

            if (sort_not_empty)
            {
                if (reverse_sort) sql += $" {_sort} DESC";
                else sql += $" {_sort}";
            }

            if (limit != -1) sql += $" {_limit}";
            if (offset != -1) sql += $" {_offset}";

            //std.info(sql);

            List<Resource> resources = new List<Resource>();
            await Task.Run(() =>
            {
                using (var con = new MySqlConnection(conString))
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, con);

                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            int pictureURLcol = reader.GetOrdinal("picture");
                            int blobPictureCol = reader.GetOrdinal("blob_picture");
                            var resource = new Resource
                            {
                                Id = reader.GetInt32("id"),
                                Name = reader.GetString("name"),
                                Description = reader.GetString("description"),
                                Category = GetCategory(reader.GetInt32("category")),
                                Price = reader.GetFloat("price"),
                                PublicationDate = reader.GetDateTime("publication_date"),
                                CreatedByUserId = reader.GetInt32("created_by")
                            };
                            if (!reader.IsDBNull(blobPictureCol))
                            {
                                byte[] imageData = (byte[])reader[blobPictureCol];
                                resource.Picture = std.GetByteImage(imageData);
                            }
                            if (!reader.IsDBNull(pictureURLcol))
                                resource.Picture = std.GetWebImage(reader.GetString("picture"));

                            resources.Add(resource);
                        }
                }
            });
            return resources;
        }
        public static Resource GetResource(string name)
        {
            using (var con = new MySqlConnection(conString))
            {
                con.Open();

                string sql = $"SELECT * FROM `resources` WHERE name=@Name";
                var cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("Name", name);
                using (var reader = cmd.ExecuteReader())
                {
                    Resource resource = null;
                    while(reader.Read()) 
                    {
                        resource = new Resource
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name"),
                            Description = reader.GetString("description"),
                            Category = GetCategory(reader.GetInt32("category")),
                            Price = reader.GetFloat("price"),
                            PublicationDate = reader.GetDateTime("publication_date"),
                            CreatedByUserId = reader.GetInt32("created_by"),
                        };
                        if (!reader.IsDBNull(reader.GetOrdinal("blob_picture")))
                        {
                            byte[] imageData = (byte[])reader[reader.GetOrdinal("blob_picture")];
                            resource.Picture = std.GetByteImage(imageData);
                        }
                        else if(!reader.IsDBNull(reader.GetOrdinal("picture")))
                            resource.Picture = std.GetWebImage(reader.GetString("picture"));
                    }
                    return resource;
                }
            }
        }
        public static void SendResource(Resource resource)
        {
            using (var con = new MySqlConnection(conString))
            {
                con.Open();

                string sql = "INSERT INTO resources_requests " +
                    "(name, description, category, request_date, blob_picture, author, price) " +
                    "VALUES (@Name, @Description, @Category, @RequestDate, @BlobPicture, @Author, @Price)";

                var cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("Name", resource.Name);
                cmd.Parameters.AddWithValue("Description", resource.Description);
                cmd.Parameters.AddWithValue("Category", resource.Category.Id);
                if (resource.Picture == null) cmd.Parameters.AddWithValue("BlobPicture", null);
                else cmd.Parameters.AddWithValue("BlobPicture", std.ConvertToByteArray(resource.Picture));
                cmd.Parameters.AddWithValue("RequestDate", DateTime.Now);
                cmd.Parameters.AddWithValue("Price", resource.Price);
                cmd.Parameters.AddWithValue("Author", resource.CreatedByUserId);

                cmd.ExecuteNonQuery();
            }
        }
        public async static Task<List<Resource>> GetResourcesOnRequest()
        {
            string sql = "SELECT * FROM resources_requests";

            List<Resource> resources = new List<Resource>();
            await Task.Run(() =>
            {
                using (var con = new MySqlConnection(conString))
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, con);

                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            int pictureURLcol = reader.GetOrdinal("picture");
                            int blobPictureCol = reader.GetOrdinal("blob_picture");
                            var resource = new Resource
                            {
                                Id = reader.GetInt32("id"),
                                Name = reader.GetString("name"),
                                Description = reader.GetString("description"),
                                Category = GetCategory(reader.GetInt32("category")),
                                Price = reader.GetFloat("price"),
                                PublicationDate = reader.GetDateTime("request_date"),
                                CreatedByUserId = reader.GetInt32("author"),
                            };

                            bool hasBlobPicture = !reader.IsDBNull(blobPictureCol);
                            bool hasPictureURL = !reader.IsDBNull(pictureURLcol);
                            string pictureURL = hasPictureURL ? reader.GetString("picture") : String.Empty;
                            if (hasBlobPicture)
                            {
                                byte[] imageData = (byte[])reader[blobPictureCol];
                                using (var ms = new MemoryStream(imageData))
                                {
                                    resource.Picture = Image.FromStream(ms);
                                }
                            }
                            else if (hasPictureURL && !string.IsNullOrEmpty(pictureURL))
                                resource.Picture = std.GetWebImage(pictureURL);

                            resources.Add(resource);
                        }
                }
            });
            return resources;
        }
        public static Resource GetResourceOnRequest(string name)
        {
            using (var con = new MySqlConnection(conString))
            {
                con.Open();

                string sql = $"SELECT * FROM `resources_requests` WHERE name=@Name";
                var cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("Name", name);
                using (var reader = cmd.ExecuteReader())
                {
                    Resource resource = null;
                    reader.Read();
                    resource = new Resource
                    {
                        Id = reader.GetInt32("id"),
                        Name = reader.GetString("name"),
                        Description = reader.GetString("description"),
                        Category = GetCategory(reader.GetInt32("category")),
                        Price = reader.GetFloat("price"),
                        PublicationDate = reader.GetDateTime("request_date"),
                        CreatedByUserId = reader.GetInt32("author"),
                    };
                    if (!reader.IsDBNull(reader.GetOrdinal("blob_picture")))
                    {
                        byte[] imageData = (byte[])reader[reader.GetOrdinal("blob_picture")];
                        resource.Picture = std.GetByteImage(imageData);
                    }
                    else if (!reader.IsDBNull(reader.GetOrdinal("picture")))
                        resource.Picture = std.GetWebImage(reader.GetString("picture"));

                    return resource;
                }
            }
        }
        public static void EditResourceOnRequest(Resource resource)
        {
            using (var con = new MySqlConnection(conString))
            {
                con.Open();

                string sql = "UPDATE resources_requests SET " +
                    "name=@Name, " +
                    "description=@Desc, " +
                    "price=@Price, " +
                    "category=@Category, " +
                    "author=@Author, " +
                    "blob_picture=@Picture " +
                    $"WHERE id={resource.Id}";

                var cmd = new MySqlCommand(sql, con);

                var resourcePicture = resource.Picture == null ? null : std.ConvertToByteArray(resource.Picture);
                cmd.Parameters.AddWithValue("Name", resource.Name);
                cmd.Parameters.AddWithValue("Desc", resource.Description);
                cmd.Parameters.AddWithValue("Price", resource.Price);
                cmd.Parameters.AddWithValue("Category", resource.Category.Id);
                cmd.Parameters.AddWithValue("Author", resource.CreatedByUserId);
                cmd.Parameters.AddWithValue("Picture", resourcePicture);

                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
        }
        public static void AddResourceOnRequest(Resource resource)
        {
            using (var con = new MySqlConnection(conString))
            {
                con.Open();

                string sql = "INSERT INTO resources_requests(" +
                             "name, " +
                             "description, " +
                             "price, " +
                             "category, " +
                             "author, " +
                             "publication_date, " +
                             "blob_picture)";

                string values = $"VALUES (@Name, " +
                                $"@Description, " +
                                $"'{resource.Price}', " +
                                $"{resource.Category.Id}, " +
                                $"{resource.CreatedByUserId}, " +
                                $"@Date, " +
                                $"@Picture);";

                sql += values;

                var cmd = new MySqlCommand(sql, con);
                var resourcePicture = resource.Picture == null ? null : std.ConvertToByteArray(resource.Picture);
                cmd.Parameters.AddWithValue("Name", resource.Name);
                cmd.Parameters.AddWithValue("Description", resource.Description);
                cmd.Parameters.AddWithValue("Date", DateTime.Now);
                cmd.Parameters.AddWithValue("Picture", resourcePicture);
                cmd.ExecuteNonQuery();
            }
        }
        public static void RemoveResource(Resource resource)
        {
            using (var con = new MySqlConnection(conString))
            {
                con.Open();

                string sql = $"DELETE FROM resources WHERE id={resource.Id}";
                var cmd = new MySqlCommand(sql, con);
                cmd.ExecuteNonQuery();
            }
        }
        public static void RemoveResource(int resource_id)
        {
            using (var con = new MySqlConnection(conString))
            {
                con.Open();

                string sql = $"DELETE FROM resources WHERE id={resource_id}";
                var cmd = new MySqlCommand(sql, con);
                cmd.ExecuteNonQuery();
            }
        }
        public static bool HasResource(string name, string table)
        {
            using (var con = new MySqlConnection(conString))
            {
                con.Open();

                string sql = $"SELECT * FROM {table} WHERE name='{name}'";
                var cmd = new MySqlCommand(sql, con);
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    return reader.HasRows;
                }
            }
        }
        public static bool HasResource(Resource res, string table)
        {
            using (var con = new MySqlConnection(conString))
            {
                con.Open();

                string sql = $"SELECT * FROM {table} WHERE name='{res.Name}'";
                var cmd = new MySqlCommand(sql, con);
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    return reader.HasRows;
                }
            }
        }
        public static void AcceptResource(Resource resource)
        {
            using (var con = new MySqlConnection(conString))
            {
                con.Open();

                string sql = "INSERT INTO resources(name, description, " +
                                "price, category, created_by, publication_date";
                if (resource.Picture == null) sql += ") ";
                else sql += ", blob_picture) ";

                string values = $"VALUES (@Name, @Description, " +
                                $"{resource.Price}, {resource.Category.Id}, {resource.CreatedByUserId}, " +
                                $"@Date";
                if (resource.Picture == null) values += ");";
                else values += ", @Picture);";

                sql += values;

                var cmd = new MySqlCommand(sql, con);
                var resourcePicture = resource.Picture == null ? null : std.ConvertToByteArray(resource.Picture);
                cmd.Parameters.AddWithValue("Name", resource.Name);
                cmd.Parameters.AddWithValue("Description", resource.Description);
                cmd.Parameters.AddWithValue("Date", DateTime.Now);
                cmd.Parameters.AddWithValue("Picture", resourcePicture);
                cmd.ExecuteNonQuery();

                string sql_remove_request = $"DELETE FROM resources_requests WHERE name=@Name";
                cmd = new MySqlCommand(sql_remove_request, con);
                cmd.Parameters.AddWithValue("Name", resource.Name);
                cmd.ExecuteNonQuery();
            }
        }
        public static void DenyResource(Resource resource)
        {
            using (var con = new MySqlConnection(conString))
            {
                con.Open();

                string sql_remove_request = $"DELETE FROM resources_requests WHERE id={resource.Id}";
                var cmd = new MySqlCommand(sql_remove_request, con);
                cmd.ExecuteNonQuery();
            }
        }
        public static void EditResource(Resource resource) 
        {
            using (var con = new MySqlConnection(conString))
            {
                con.Open();

                string sql = "UPDATE resources SET " +
                    "name=@Name, " +
                    "description=@Desc, " +
                    "price=@Price, " +
                    "category=@Category, " +
                    "created_by=@Author, " +
                    "blob_picture=@Picture " +
                    $"WHERE id={resource.Id}";

                var cmd = new MySqlCommand(sql, con);

                var resourcePicture = resource.Picture == null ? null : std.ConvertToByteArray(resource.Picture);
                cmd.Parameters.AddWithValue("Name", resource.Name);
                cmd.Parameters.AddWithValue("Desc", resource.Description);
                cmd.Parameters.AddWithValue("Price", resource.Price);
                cmd.Parameters.AddWithValue("Category", resource.Category.Id);
                cmd.Parameters.AddWithValue("Author", resource.CreatedByUserId);
                cmd.Parameters.AddWithValue("Picture", resourcePicture);

                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
        }
        public static void AddResource(Resource resource)
        {
            using (var con = new MySqlConnection(conString))
            {
                con.Open();

                string sql = "INSERT INTO resources(" +
                             "name, " +
                             "description, " +
                             "price, " +
                             "category, " +
                             "created_by, " +
                             "publication_date, " +
                             "blob_picture)";

                string values = $"VALUES (@Name, " +
                                $"@Description, " +
                                $"'{resource.Price}', " +
                                $"{resource.Category.Id}, " +
                                $"{resource.CreatedByUserId}, " +
                                $"@Date, " +
                                $"@Picture);";

                sql += values;

                var cmd = new MySqlCommand(sql, con);
                var resourcePicture = resource.Picture == null ? null : std.ConvertToByteArray(resource.Picture);
                cmd.Parameters.AddWithValue("Name", resource.Name);
                cmd.Parameters.AddWithValue("Description", resource.Description);
                cmd.Parameters.AddWithValue("Date", DateTime.Now);
                cmd.Parameters.AddWithValue("Picture", resourcePicture);
                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region payment
        public static void MakePurchase(Entities.User user, List<Resource> resources)
        {
            using (var con = new MySqlConnection(conString))
            {
                con.Open();

                foreach (var resource in resources)
                {
                    string check = $"SELECT * FROM purchases WHERE user_id={user.Id} AND resource_id={resource.Id}";
                    var cmd = new MySqlCommand(check, con);
                    using (var reader = cmd.ExecuteReader()) 
                    {
                        if (reader.HasRows) continue;
                    }

                    string sql = "INSERT INTO `purchases`(resource_id, user_id, purchase_date, cost) " +
                        $"VALUES({resource.Id}, {user.Id}, @Date, {resource.Price})";

                    cmd = new MySqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("Date", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void MakePurchase(Entities.User user, Resource resources)
        {
            List<Resource> resourcesList = new List<Resource>();
            resourcesList.Add(resources);
            MakePurchase(user, resourcesList);
        }

        #endregion

        #region handbooks
        /// <summary>
        /// Получает наименования из указанного в параметре справочника. Справочник - таблица содержащая только id и name
        /// </summary>
        /// <param name="handbook"></param>
        /// <returns></returns>
        public static List<Handbook> GetHandbookData(string handbook)
        {
            List<Handbook> data = new List<Handbook>();
            using (var con = new MySqlConnection(conString))
            {
                con.Open();
                string sql = $"SELECT * FROM {handbook}";
                var cmd = new MySqlCommand(sql, con);
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        data.Add(new Handbook
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name")
                        });
                    }
            }
            return data;
        }
        /// <summary>
        /// Создаёт в базе данных запись из объекта справочника.
        /// </summary>
        /// <param name="handbook"></param>
        public static void CreateHandbookItem(string handbook, Handbook item)
        {
            using (var con = new MySqlConnection(conString))
            {
                con.Open();
                string sql = $"INSERT INTO {handbook} (name) VALUES (@Name)";

                var cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("Name", item.Name);
                cmd.ExecuteNonQuery();
            }
        }
        public static void UpdateHandbookItem(string handbook, Handbook item)
        {
            using (var con = new MySqlConnection(conString))
            {
                con.Open();
                string sql = $"UPDATE {handbook} SET name=@Name WHERE id=@Id";

                var cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("Name", item.Name);
                cmd.Parameters.AddWithValue("Id", item.Id);
                cmd.ExecuteNonQuery();
            }
        }
        public static Handbook GetHandbookItem(string table, string name)
        {
            using (var con = new MySqlConnection(conString))
            {
                con.Open();

                string sql = $"SELECT * FROM {table} WHERE name=@Name";
                var cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("Name", name);
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        return new Handbook
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name")
                        };
                    }
            }
            return null;
        }
        #endregion

        #region roles
        public static List<Role> GetRoles()
        {
            List<Role> roles = new List<Role>();
            using (var con = new MySqlConnection(conString))
            {
                con.Open();

                string sql = "SELECT * FROM `roles`";
                var cmd = new MySqlCommand(sql, con);
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        roles.Add(new Role
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name")
                        });
                    }
            }
            return roles;
        }
        public static Role GetRole(string name)
        {
            using (var con = new MySqlConnection(conString))
            {
                con.Open();

                string sql = $"SELECT * FROM roles WHERE name=@name";
                var cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("name", name);
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    return new Role
                    {
                        Id = reader.GetInt32("id"),
                        Name = reader.GetString("name")
                    };
                }
            }
        }
        public static Role GetRole(int id)
        {
            using (var con = new MySqlConnection(conString))
            {
                con.Open();

                string sql = $"SELECT * FROM roles WHERE id={id}";
                var cmd = new MySqlCommand(sql, con);
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    return new Role
                    {
                        Id = reader.GetInt32("id"),
                        Name = reader.GetString("name")
                    };
                }
            }
        }
        #endregion

        #region categories
        public static List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();
            using (var con = new MySqlConnection(conString))
            {
                con.Open();

                string sql = "SELECT * FROM `categories`";
                var cmd = new MySqlCommand(sql, con);
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        var category = new Category
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name")
                        };

                        categories.Add(category);
                    }
            }
            return categories;
        }
        public static Category GetCategory(int id)
        {
            using (var con = new MySqlConnection(conString))
            {
                con.Open();

                string sql = $"SELECT * FROM categories WHERE id={id}";
                var cmd = new MySqlCommand(sql, con);
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    return new Category
                    {
                        Id = reader.GetInt32("id"),
                        Name = reader.GetString("name")
                    };
                }
            }
        }
        public static Category GetCategory(string name)
        {
            using (var con = new MySqlConnection(conString))
            {
                con.Open();

                string sql = $"SELECT * FROM categories WHERE name=@name";
                var cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("name", name);
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    return new Category
                    {
                        Id = reader.GetInt32("id"),
                        Name = reader.GetString("name")
                    };
                }
            }
        }
        #endregion

        public static long GetRecordsCount(string table)
        {
            using (var con = new MySqlConnection(conString))
            {
                con.Open();
                string sql = $"SELECT COUNT(*) FROM {table}";

                var cmd = new MySqlCommand(sql, con);
                return cmd.ExecuteNonQuery();
            }
        }
        
        public static List<TableColumn> GetTableColumns(string table)
        {
            List<TableColumn> columns = new List<TableColumn>();
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                con.Open();

                string sql = $"SHOW COLUMNS FROM `{table}`;";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.HasRows) return null;
                    while (reader.Read())
                    {
                        columns.Add(new TableColumn
                        {
                            Name = reader.GetString("Field"),
                            Type = reader.GetString("Type"),
                            Nullable = reader.GetString("Null") == "YES",
                            Extra = reader.GetString("Extra")
                        });
                    }
                }
            }
            return columns;
        }
        public static List<string> GetTables() 
        {
            using (var con = new MySqlConnection(conString))
            {
                con.Open();

                string sql = $"SHOW TABLES FROM {database}";
                var cmd = new MySqlCommand(sql, con);
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.HasRows) return null;
                    var tables = new List<string>();
                    while (reader.Read())
                    {
                        tables.Add(reader[0].ToString());
                    }
                    return tables;
                }
            }
        }


        /// <summary>
        /// Получает массив заголовков из указнного csv файла
        /// </summary>
        public static string[] CSV_GetFileColumns(string filepath, char delimeter = ';')
        {
            if (!File.Exists(filepath))
            {
                std.error($"Указанный файл несуществует.\n{filepath}");
                return null;
            }
            var reader = new StreamReader(filepath);
            string importFile = reader.ReadLine();
            string[] headers = importFile.Split(delimeter);
            reader.Close();
            return headers;
        }


        /// <summary>
        /// Считывает csv файл и преобразует его в sql запрос типа "INSERT INTO"
        /// </summary>
        /// <param name="filepath">Путь к файлу</param>
        /// <param name="table">Таблица, в которую будет произведён импорт</param>
        /// <param name="delimeter">Символ-разделитель в csv файле</param>
        public static void ImportData(string filepath, string table, char delimeter = ';')
        {
            if (!File.Exists(filepath))
            {
                std.error($"Указанный файл несуществует.\n{filepath}");
                return;
            }

            var tableColumns = GetTableColumns(table);
            var missingColumns = new List<TableColumn>();
            var csvColumns = CSV_GetFileColumns(filepath, delimeter);

            // записываем пропущенные в csv файле колонки
            foreach (var column in tableColumns)
            {
                if (!csvColumns.Contains(column.Name)) missingColumns.Add(column);
            }

            // проверяем наличие пропущенных полей в файле спрашиваем у пользователя, хочет ли он продолжить
            if (missingColumns.Count > 0)
            {
                DialogResult ans = std.warning($"Количество параметров в файле не соответствует количеству параметров в таблице. Хотите попытаться импортировать данные игнорируя пропущенные параметры?");
                if (ans == DialogResult.No) return;
            }

            // проверяем свойства пропущенных полей
            missingColumns?.ForEach(column => 
            {
                if (!column.Nullable)
                {
                    if (column.IsForeignKey() || string.IsNullOrWhiteSpace(column.Name))
                    {
                        std.error("Импорт данных невозможен.");
                        return;
                    }
                    if (column.IsPrimaryKey() && !column.HasAutoIncrement())
                    {
                        std.error("Импорт данных невозможен.");
                        return;
                    }
                }
            });

            // удаляем пропущенные поля из списка
            missingColumns?.ForEach(column => 
            {
                if (tableColumns.Contains(column)) { tableColumns.Remove(column); }
            });

            // так как в csv файле и таблице колонки могут идти в разном порядке,
            // для удобного формирования корректного sql запроса
            // распределяем информацию по колонкам в соответствии с таблицей.
            int[] arraySortMethod = new int[tableColumns.Count];
            for (int i = 0; i < tableColumns.Count; i++)
            {
                if (tableColumns[i].Name != csvColumns[i])
                {
                    for (int k = 0; k < csvColumns.Length; k++)
                    {
                        if (tableColumns[i].Name == csvColumns[k])
                        {
                            arraySortMethod[i] = k;
                            break;
                        }
                    }
                    continue;
                }
                arraySortMethod[i] = i;
            }

            List<string> sorted = new List<string>();
            
            // зная в каком порядке расположены колонки файла относительно колонок таблицы,
            // для большего удобства записываем информацию в отдельный список в правильном порядке
            using (var reader = new StreamReader(filepath))
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null) break;
                    string[] data = line.Split(delimeter);
                    for (int i = 0; i < data.Length; i++)
                    {
                        try
                        {
                            sorted.Add(data[arraySortMethod[i]]);
                        }
                        catch
                        {
                            std.error("При чтении файла возникла неизвестная ошибка.");
                            return;
                        }
                    }
                }
            }
            
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                con.Open();

                string columns = "";
                tableColumns.ForEach(column =>
                {
                    if (tableColumns.IndexOf(column) != 0) columns += ",";
                    columns += column.Name;
                });

                string requestBody = $"INSERT INTO `{table}` ({columns}) VALUES ";
                string sql;
                int recordsImported = 0;
                //пропускаем заголовки и "построчно" считываем файл.
                for (int i = tableColumns.Count; i < sorted.Count; i += tableColumns.Count)
                {
                    sql = requestBody;
                    sql += "(";
                    for (int k = 0; k < tableColumns.Count; k++)
                    {
                        if (k != 0) sql += ", ";
                        string item = sorted[i + k];
                        sql += $"\'{item}\'";
                    }
                    sql += ");";
                    
                    try
                    {
                        var cmd = new MySqlCommand(sql, con);
                        cmd.ExecuteNonQuery();
                        recordsImported++;
                        continue;
                    }
                    catch
                    {
                        continue;
                    }
                }
                std.info($"Импортировано записей: {recordsImported}");
            }
        }

        public static bool ExecuteScript(string filepath)
        {
            try
            {
                using (var con = new MySqlConnection(conStringNoDB))
                {
                    con.Open();

                    string sql = File.ReadAllText(filepath);
                    var cmd = new MySqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static string GetDatabaseFromScript(string filepath)
        {
            string[] keyWords = "CREATE DATABASE IF NOT EXISTS USE".Split(' ');
            using (var reader = new StreamReader(filepath)) 
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null) return null;
                    string[] words = line.Split(' ');
                    bool targetLine = false;
                    foreach(string word in words) 
                    { 
                        // проверяем строку на наличие ключевых слов
                        if (keyWords.Contains(word))
                        {
                            // если ключевое слово найдено, значит мы находимся на нужной строке
                            targetLine = true;
                        }
                        if (targetLine)
                        {
                            // если мы находимся на нужной строке, значит нам нужно искать единственное слово,
                            // которое не является ключевым
                            if (!keyWords.Contains(word) && !string.IsNullOrWhiteSpace(word))
                            {
                                string temp = word;
                                if (temp[0] == '`') temp = temp.Remove(0, 1);
                                if (temp[temp.Length - 1 ]== '`') temp = temp.Remove(temp.Length - 1, 1);
                                return temp;
                            }
                        }
                    }
                }
            }
        }
        public static bool DatabaseExistsOnServer(string name)
        {
            using (var con = new MySqlConnection(conStringNoDB))
            {
                con.Open();
               
                string sql = $"SHOW SCHEMAS WHERE `Database`='{name}'";
                var cmd = new MySqlCommand(sql, con);
                using (var reader = cmd.ExecuteReader())
                {
                    return reader.HasRows;
                }
            }
        }
    }
}