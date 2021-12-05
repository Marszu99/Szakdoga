using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using TimeSheet.Model;

namespace TimeSheet.DataAccess
{
    public class UserLogic : IUserLogic
    {
        public int RegisterUser(User user, string password2, string email2, string companyName2)
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {
                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("demofeladat.CreateUser", connection);

                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.Parameters.Add(new MySqlParameter("@Username", MySqlDbType.VarChar));
                myCmd.Parameters.Add(new MySqlParameter("@Password", MySqlDbType.VarChar));
                myCmd.Parameters.Add(new MySqlParameter("@FirstName", MySqlDbType.VarChar));
                myCmd.Parameters.Add(new MySqlParameter("@LastName", MySqlDbType.VarChar));
                myCmd.Parameters.Add(new MySqlParameter("@Email", MySqlDbType.VarChar));
                myCmd.Parameters.Add(new MySqlParameter("@Telephone", MySqlDbType.VarChar));

                myCmd.Parameters["@Username"].Value = user.Username;
                myCmd.Parameters["@Password"].Value = user.Password;
                myCmd.Parameters["@FirstName"].Value = user.FirstName;
                myCmd.Parameters["@LastName"].Value = user.LastName;
                myCmd.Parameters["@Email"].Value = user.Email;
                myCmd.Parameters["@Telephone"].Value = user.Telephone;

                return Convert.ToInt32(myCmd.ExecuteScalar());
            }
        }

        public int CreateUser(User user)
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {
                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("demofeladat.CreateUser", connection);

                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.Parameters.Add(new MySqlParameter("@Username", MySqlDbType.VarChar));
                myCmd.Parameters.Add(new MySqlParameter("@Password", MySqlDbType.VarChar));
                myCmd.Parameters.Add(new MySqlParameter("@FirstName", MySqlDbType.VarChar));
                myCmd.Parameters.Add(new MySqlParameter("@LastName", MySqlDbType.VarChar));
                myCmd.Parameters.Add(new MySqlParameter("@Email", MySqlDbType.VarChar));
                myCmd.Parameters.Add(new MySqlParameter("@Telephone", MySqlDbType.VarChar));

                myCmd.Parameters["@Username"].Value = user.Username;
                myCmd.Parameters["@Password"].Value = user.Password;
                myCmd.Parameters["@FirstName"].Value = user.FirstName;
                myCmd.Parameters["@LastName"].Value = user.LastName;
                myCmd.Parameters["@Email"].Value = user.Email;
                myCmd.Parameters["@Telephone"].Value = user.Telephone;

                return Convert.ToInt32(myCmd.ExecuteScalar());
            }
        }

        public List<User> GetAllUsers()
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {
                List<User> users = new List<User>();

                DataTable dt = new DataTable();
                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("demofeladat.GetAllUsers", connection);
                myCmd.CommandType = CommandType.StoredProcedure;
                MySqlDataReader sdr = myCmd.ExecuteReader();

                dt.Load(sdr);

                foreach (DataRow dr in dt.Rows)
                {
                    User user = new User();
                    user.IdUser = int.Parse(dr["IdUser"].ToString());
                    user.Username = dr["UserName"].ToString();
                    user.Password = dr["Password"].ToString();
                    user.FirstName = dr["FirstName"].ToString();
                    user.LastName = dr["LastName"].ToString();
                    user.Email = dr["Email"].ToString();
                    user.Telephone = dr["Telephone"].ToString();

                    users.Add(user);
                }

                return users;
            }
        }

        public void UpdateUser(User user)
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {              
                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("demofeladat.UpdateUser", connection);

                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.Parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32));
                myCmd.Parameters.Add(new MySqlParameter("@Password", MySqlDbType.VarChar));
                myCmd.Parameters.Add(new MySqlParameter("@FirstName", MySqlDbType.VarChar));
                myCmd.Parameters.Add(new MySqlParameter("@LastName", MySqlDbType.VarChar));
                myCmd.Parameters.Add(new MySqlParameter("@Email", MySqlDbType.VarChar));
                myCmd.Parameters.Add(new MySqlParameter("@Telephone", MySqlDbType.VarChar));

                myCmd.Parameters["@id"].Value = user.IdUser;
                myCmd.Parameters["@Password"].Value = user.Password;
                myCmd.Parameters["@FirstName"].Value = user.FirstName;
                myCmd.Parameters["@LastName"].Value = user.LastName;
                myCmd.Parameters["@Email"].Value = user.Email;
                myCmd.Parameters["@Telephone"].Value = user.Telephone;

                myCmd.ExecuteNonQuery();
            }
        }

        public void DeleteUser(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {
                //int id = (myDataGrid.SelectedItem as User).IdUser;

                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("demofeladat.DeleteUser", connection);
                myCmd.Parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32));
                myCmd.Parameters["@id"].Value = id;
                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.ExecuteNonQuery();
            }
        }

        public bool IsValidLogin(string username, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {

                DataTable dt = new DataTable();
                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("demofeladat.GetAllUsers", connection);
                myCmd.CommandType = CommandType.StoredProcedure;
                MySqlDataReader sdr = myCmd.ExecuteReader();

                dt.Load(sdr);

                foreach (DataRow dr in dt.Rows)
                {
                    if (username == dr["UserName"].ToString() && password == dr["Password"].ToString())// && loginUser.IdUser.ToString() == myCmd.Parameters["@id"].Value)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public User GetUserByUsername(string username)
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {
                User user = new User();
                DataTable dt = new DataTable();
                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("demofeladat.GetUserByUsername", connection);
                myCmd.Parameters.Add(new MySqlParameter("@username", MySqlDbType.VarChar));
                myCmd.Parameters["@username"].Value = username;
                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.ExecuteNonQuery();

                MySqlDataReader sdr = myCmd.ExecuteReader();

                dt.Load(sdr);

                foreach (DataRow dr in dt.Rows)
                {
                    user.IdUser = int.Parse(dr["IdUser"].ToString());
                    user.Username = dr["UserName"].ToString();
                    user.Password = dr["Password"].ToString();
                    user.FirstName = dr["FirstName"].ToString();
                    user.LastName = dr["LastName"].ToString();
                    user.Email = dr["Email"].ToString();
                    user.Telephone = dr["Telephone"].ToString();
                }

                return user;
            }
        }
    }
}
