using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using TimeSheet.Model;

namespace TimeSheet.DataAccess
{
    public class TaskLogic : ITaskLogic
    {
        public int CreateTask(Task task, int userid)
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {
                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("szakdoga.CreateTaskForUser", connection);

                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.Parameters.Add(new MySqlParameter("@title", MySqlDbType.VarChar));
                myCmd.Parameters.Add(new MySqlParameter("@description", MySqlDbType.Text));
                myCmd.Parameters.Add(new MySqlParameter("@deadline", MySqlDbType.Date));
                myCmd.Parameters.Add(new MySqlParameter("@userid", MySqlDbType.Int32));


                myCmd.Parameters["@title"].Value = task.Title;
                myCmd.Parameters["@description"].Value = task.Description;
                myCmd.Parameters["@deadline"].Value = task.Deadline;
                myCmd.Parameters["@userid"].Value = userid;

                return Convert.ToInt32(myCmd.ExecuteScalar());
            }
        }

        public List<Task> GetAllTasks()
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {
                List<Task> tasks = new List<Task>();

                DataTable dt = new DataTable();
                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("szakdoga.GetAllTasks", connection);
                myCmd.CommandType = CommandType.StoredProcedure;


                MySqlDataReader sdr = myCmd.ExecuteReader();

                dt.Load(sdr);

                foreach (DataRow dr in dt.Rows)
                {
                    Task task = new Task();
                    task.IdTask = int.Parse(dr["idTask"].ToString());
                    task.Title = dr["Title"].ToString();
                    task.Description = dr["Description"].ToString();
                    task.Deadline = DateTime.Parse(dr["Deadline"].ToString());
                    task.Status = (TaskStatus)Enum.Parse(typeof(TaskStatus), dr["Status"].ToString());
                    task.CreationDate = DateTime.Parse(dr["CreationDate"].ToString());
                    task.User_idUser = int.Parse(dr["User_idUser"].ToString());
                    task.User_Username = dr["Username"].ToString();

                    tasks.Add(task);
                }

                return tasks;
            }
        }

        public List<Task> GetUserTasks(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {
                List<Task> tasks = new List<Task>();

                DataTable dt = new DataTable();
                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("szakdoga.GetUserTasks", connection);
                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.Parameters.Add(new MySqlParameter("@userid", MySqlDbType.Int32));
                myCmd.Parameters["@userid"].Value = id;

                MySqlDataReader sdr = myCmd.ExecuteReader();

                dt.Load(sdr);

                foreach (DataRow dr in dt.Rows)
                {
                    Task task = new Task();
                    task.IdTask = int.Parse(dr["idTask"].ToString());
                    task.Title = dr["Title"].ToString();
                    task.Description = dr["Description"].ToString();
                    task.Deadline = DateTime.Parse(dr["Deadline"].ToString());
                    task.Status = (TaskStatus)Enum.Parse(typeof(TaskStatus), dr["Status"].ToString());
                    task.CreationDate = DateTime.Parse(dr["CreationDate"].ToString());
                    task.User_idUser = int.Parse(dr["User_idUser"].ToString());
                    task.User_Username = dr["Username"].ToString();

                    tasks.Add(task);
                }

                return tasks;
            }
        }

        public List<Task> GetAllActiveTasks()
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {
                List<Task> tasks = new List<Task>();

                DataTable dt = new DataTable();
                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("szakdoga.GetAllActiveTasks", connection);
                myCmd.CommandType = CommandType.StoredProcedure;

                MySqlDataReader sdr = myCmd.ExecuteReader();

                dt.Load(sdr);

                foreach (DataRow dr in dt.Rows)
                {
                    Task task = new Task();
                    task.IdTask = int.Parse(dr["idTask"].ToString());
                    task.Title = dr["Title"].ToString();
                    task.Description = dr["Description"].ToString();
                    task.Deadline = DateTime.Parse(dr["Deadline"].ToString());
                    task.Status = (TaskStatus)Enum.Parse(typeof(TaskStatus), dr["Status"].ToString());
                    task.CreationDate = DateTime.Parse(dr["CreationDate"].ToString());
                    task.User_idUser = int.Parse(dr["User_idUser"].ToString());
                    task.User_Username = dr["Username"].ToString();

                    tasks.Add(task);
                }

                return tasks;
            }
        }

        public List<Task> GetAllActiveTasksFromUser(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {
                List<Task> tasks = new List<Task>();

                DataTable dt = new DataTable();
                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("szakdoga.GetAllActiveTasksFromUser", connection);
                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.Parameters.Add(new MySqlParameter("@userid", MySqlDbType.Int32));
                myCmd.Parameters["@userid"].Value = id;

                MySqlDataReader sdr = myCmd.ExecuteReader();

                dt.Load(sdr);

                foreach (DataRow dr in dt.Rows)
                {
                    Task task = new Task();
                    task.IdTask = int.Parse(dr["idTask"].ToString());
                    task.Title = dr["Title"].ToString();
                    task.Description = dr["Description"].ToString();
                    task.Deadline = DateTime.Parse(dr["Deadline"].ToString());
                    task.Status = (TaskStatus)Enum.Parse(typeof(TaskStatus), dr["Status"].ToString());
                    task.CreationDate = DateTime.Parse(dr["CreationDate"].ToString());
                    task.User_idUser = int.Parse(dr["User_idUser"].ToString());
                    //task.User_idUser = id; Melyik szebb???

                    tasks.Add(task);
                }

                return tasks;
            }
        }

        public List<Task> GetAllDoneTasksFromUser(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {
                List<Task> tasks = new List<Task>();

                DataTable dt = new DataTable();
                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("szakdoga.GetAllDoneTasksFromUser", connection);
                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.Parameters.Add(new MySqlParameter("@userid", MySqlDbType.Int32));
                myCmd.Parameters["@userid"].Value = id;

                MySqlDataReader sdr = myCmd.ExecuteReader();

                dt.Load(sdr);

                foreach (DataRow dr in dt.Rows)
                {
                    Task task = new Task();
                    task.IdTask = int.Parse(dr["idTask"].ToString());
                    task.Title = dr["Title"].ToString();
                    task.Description = dr["Description"].ToString();
                    task.Deadline = DateTime.Parse(dr["Deadline"].ToString());
                    task.Status = (TaskStatus)Enum.Parse(typeof(TaskStatus), dr["Status"].ToString());
                    task.CreationDate = DateTime.Parse(dr["CreationDate"].ToString());

                    tasks.Add(task);
                }

                return tasks;
            }
        }

        public void UpdateTask(Task task, int taskid, int userid)
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {
                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("szakdoga.UpdateTask", connection);

                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.Parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32));
                myCmd.Parameters.Add(new MySqlParameter("@title", MySqlDbType.VarChar));
                myCmd.Parameters.Add(new MySqlParameter("@description", MySqlDbType.Text));
                myCmd.Parameters.Add(new MySqlParameter("@deadline", MySqlDbType.Date));
                myCmd.Parameters.Add(new MySqlParameter("@status", MySqlDbType.VarChar));
                myCmd.Parameters.Add(new MySqlParameter("@userid", MySqlDbType.Int32));

                myCmd.Parameters["@id"].Value = taskid;
                myCmd.Parameters["@title"].Value = task.Title;
                myCmd.Parameters["@description"].Value = task.Description;
                myCmd.Parameters["@deadline"].Value = task.Deadline;
                myCmd.Parameters["@status"].Value = task.Status;
                myCmd.Parameters["@userid"].Value = userid;

                myCmd.ExecuteNonQuery();
            }
        }

        public void DeleteTask(int taskid)
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {
                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("szakdoga.DeleteTask", connection);
                myCmd.Parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32));
                myCmd.Parameters["@id"].Value = taskid;
                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.ExecuteNonQuery();
            }
        }
    }
}
