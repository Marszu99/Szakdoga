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

        public string GetTaskNotifications(int taskid)//public List<Notification> GetTaskNotifications(int taskid)
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {
                //List<Notification> notifications = new List<Notification>();
                string notifcationMessage = null;

                DataTable dt = new DataTable();
                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("szakdoga.GetTaskNotifications", connection);
                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.Parameters.Add(new MySqlParameter("@taskid", MySqlDbType.Int32));
                myCmd.Parameters["@taskid"].Value = taskid;


                MySqlDataReader sdr = myCmd.ExecuteReader();

                dt.Load(sdr);

                int count = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    //Notification notification = new Notification();
                    //notification.IdNotification = int.Parse(dr["IdNotification"].ToString());
                    //notification.Message = dr["Message"].ToString();

                    if (count > 1) notifcationMessage += "\n";
                    notifcationMessage += dr["Message"].ToString();
                    count++;

                    //notifications.Add(notification);
                }

                //return notifications;
                return notifcationMessage;
            }
        }

        public string GetTaskNotificationsForEmployee(int taskid)//public List<Notification> GetTaskNotifications(int taskid)
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {
                //List<Notification> notifications = new List<Notification>();
                string notifcationMessage = null;

                DataTable dt = new DataTable();
                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("szakdoga.GetTaskNotificationsForEmployee", connection);
                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.Parameters.Add(new MySqlParameter("@taskid", MySqlDbType.Int32));
                myCmd.Parameters["@taskid"].Value = taskid;


                MySqlDataReader sdr = myCmd.ExecuteReader();

                dt.Load(sdr);

                int count = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    //Notification notification = new Notification();
                    //notification.IdNotification = int.Parse(dr["IdNotification"].ToString());
                    //notification.Message = dr["Message"].ToString();

                    if (count > 1) notifcationMessage += "\n";
                    notifcationMessage += dr["Message"].ToString();
                    count++;

                    //notifications.Add(notification);
                }

                //return notifications;
                return notifcationMessage;
            }
        }

        public string GetTaskNotificationsForAdmin(int taskid)//public List<Notification> GetTaskNotifications(int taskid)
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {
                //List<Notification> notifications = new List<Notification>();
                string notifcationMessage = null;

                DataTable dt = new DataTable();
                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("szakdoga.GetTaskNotificationsForAdmin", connection);
                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.Parameters.Add(new MySqlParameter("@taskid", MySqlDbType.Int32));
                myCmd.Parameters["@taskid"].Value = taskid;


                MySqlDataReader sdr = myCmd.ExecuteReader();

                dt.Load(sdr);

                int count = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    //Notification notification = new Notification();
                    //notification.IdNotification = int.Parse(dr["IdNotification"].ToString());
                    //notification.Message = dr["Message"].ToString();

                    if (count > 1) notifcationMessage += "\n";
                    notifcationMessage += dr["Message"].ToString();
                    count++;

                    //notifications.Add(notification);
                }

                //return notifications;
                return notifcationMessage;
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
