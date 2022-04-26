using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using TimeSheet.Model;

namespace TimeSheet.DataAccess
{
    public class NotificationLogic : INotificationLogic
    {
        public int CreateNotificationForTask(string message, int notificationFor, int taskid)
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {
                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("szakdoga.CreateNotificationForTask", connection);

                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.Parameters.Add(new MySqlParameter("@message", MySqlDbType.Text));
                myCmd.Parameters.Add(new MySqlParameter("@notificationFor", MySqlDbType.Int32));
                myCmd.Parameters.Add(new MySqlParameter("@taskid", MySqlDbType.Int32));


                myCmd.Parameters["@message"].Value = message;
                myCmd.Parameters["@notificationFor"].Value = notificationFor;
                myCmd.Parameters["@taskid"].Value = taskid;


                return Convert.ToInt32(myCmd.ExecuteScalar());
            }
        }

        public Notification GetNotificationByID(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {
                Notification notification = new Notification();

                DataTable dt = new DataTable();
                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("szakdoga.GetNotificationByID", connection);
                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.Parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32));
                myCmd.Parameters["@id"].Value = id;
                myCmd.ExecuteNonQuery();

                MySqlDataReader sdr = myCmd.ExecuteReader();

                dt.Load(sdr);

                foreach (DataRow dr in dt.Rows)
                {
                    notification.Message = dr["Message"].ToString();
                    notification.NotificationFor = int.Parse(dr["NotificationFor"].ToString());
                    notification.Task_idTask = int.Parse(dr["Task_idTask"].ToString());
                }
                return notification;
            }
        }

        public List<string> GetTaskNotificationsForEmployee(int taskid)
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {
                List<string> notifications = new List<string>();

                DataTable dt = new DataTable();
                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("szakdoga.GetTaskNotificationsForEmployee", connection);
                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.Parameters.Add(new MySqlParameter("@taskid", MySqlDbType.Int32));
                myCmd.Parameters["@taskid"].Value = taskid;


                MySqlDataReader sdr = myCmd.ExecuteReader();

                dt.Load(sdr);

                foreach (DataRow dr in dt.Rows)
                {
                    string notifcationMessage = dr["Message"].ToString();

                    notifications.Add(notifcationMessage);
                }
                return notifications;
            }
        }

        public List<string> GetTaskNotificationsForAdmin(int taskid)
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {
                List<string> notifications = new List<string>();

                DataTable dt = new DataTable();
                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("szakdoga.GetTaskNotificationsForAdmin", connection);
                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.Parameters.Add(new MySqlParameter("@taskid", MySqlDbType.Int32));
                myCmd.Parameters["@taskid"].Value = taskid;


                MySqlDataReader sdr = myCmd.ExecuteReader();

                dt.Load(sdr);

                foreach (DataRow dr in dt.Rows)
                {
                    string notifcationMessage = dr["Message"].ToString();

                    notifications.Add(notifcationMessage);
                }
                return notifications;
            }
        }


        public void HasReadNotification(int taskid, int notificationFor)//notificationid
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {
                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("szakdoga.HasReadNotification", connection);
                myCmd.Parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32));
                myCmd.Parameters.Add(new MySqlParameter("@notificationFor", MySqlDbType.Int32));
                myCmd.Parameters["@id"].Value = taskid;
                myCmd.Parameters["@notificationFor"].Value = notificationFor;
                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.ExecuteNonQuery();
            }
        }
    }
}
