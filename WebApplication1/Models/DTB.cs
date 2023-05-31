namespace WebApplication1.Models
{
    using System.Data.SQLite;

    public class SQLiteManager
    {
        private string connectionString;

        public SQLiteManager(string dbPath)
        {
            connectionString = string.Format("Data Source={0};Version=3;", dbPath);
        }

        public User GetUsers(User model)
        {
            User users = null;
            
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Users WHERE UserName=@UserName AND PassWord=@PassWord";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", model.UserName);
                    command.Parameters.AddWithValue("@PassWord", model.PassWord);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            users = new User(); // Gán giá trị cho users nếu có bản ghi phù hợp
                            users.ID = reader.GetInt32(0);
                            users.UserName = reader.GetString(1);
                            users.PassWord = reader.GetString(2);
                            string role = reader.GetString(5); // Ở đây, giả sử cột role có chỉ số là 5
                            if (role == "admin")
                            {
                                // Người dùng có vai trò là admin
                                users.Role = "admin";
                            }
                            else
                            {
                                // Người dùng có vai trò khác
                                users.Role = "user";
                            }
                        }
                    }
                }

                connection.Close();
            }

            return users;
        }
        public User CheckUsers(User model)
        {
            User users = null;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Users WHERE UserName=@UserName AND Password=@PassWord AND Email=@Email AND Mobile=@Mobile";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", model.UserName);
                    command.Parameters.AddWithValue("@PassWord", model.PassWord);
                    command.Parameters.AddWithValue("@Email", model.Email);
                    command.Parameters.AddWithValue("@Mobile", model.Mobile);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            users = new User(); // Gán giá trị cho users nếu có bản ghi phù hợp
                            users.UserName = reader.GetString(1);
                            users.PassWord = reader.GetString(2);
                            users.Email = reader.GetString(3);
                            users.Mobile = reader.GetInt32(4);
                        }
                    }
                }

                connection.Close();
            }

            return users;
        }

        public void AddUser(User model)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Users (UserName, PassWord, Email, Mobile, Role) VALUES (@UserName, @PassWord, @Email, @Mobile, @Role)";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", model.UserName);
                    command.Parameters.AddWithValue("@PassWord", model.PassWord);
                    command.Parameters.AddWithValue("@Email", model.Email);
                    command.Parameters.AddWithValue("@Mobile", model.Mobile);
                    command.Parameters.AddWithValue("@Role", "user");

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
        /*public bool CheckEmail(string email)
        {
            bool isEmailExists = false;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if (count > 0)
                    {
                        isEmailExists = true;
                    }
                }
                connection.Close();
            }
            return isEmailExists;
        }*/
        public User CheckUsers1(User model)
        {
            User users = null;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Users WHERE UserName=@UserName AND Email=@Email AND Mobile=@Mobile";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", model.UserName);
                    command.Parameters.AddWithValue("@Email", model.Email);
                    command.Parameters.AddWithValue("@Mobile", model.Mobile);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            users = new User(); // Gán giá trị cho users nếu có bản ghi phù hợp
                            users.UserName = reader.GetString(1);
                            users.Email = reader.GetString(3);
                            users.Mobile = reader.GetInt32(4);
                        }
                    }
                }

                connection.Close();
            }

            return users;
        }
        public void UpdatePassword(string email, string newPassword)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE Users SET PassWord = @NewPassword WHERE Email = @Email";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NewPassword", newPassword);
                    command.Parameters.AddWithValue("@Email", email);

                    command.ExecuteNonQuery();
                }
            }
        }
        public void saveUser(string tenTruycap, string email, string sodienthoai)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Users SET Email=@email, Mobile=@mobile WHERE UserName=@id";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@mobile", sodienthoai);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@id", tenTruycap);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

        }
        public User GetUsers(object id)
        {
            User users = new User();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Users WHERE ID=@ID";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.ID = reader.GetInt32(0);
                            users.UserName = reader.GetString(1);
                            users.PassWord = reader.GetString(2);
                            users.Email = reader.GetString(3);
                            users.Mobile = reader.GetInt32(4);
                            users.Role = reader.GetString(5);
                        }
                    }
                }

                connection.Close();
            }
            return users;
        }
    }

}
