using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using TimeSheet.Model;

namespace TimeSheet.DataAccess
{
    public class RecordLogic : IRecordLogic
    {
        public int CreateRecord(Record record, int userid, int taskid)
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {
                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("szakdoga.CreateRecord", connection);

                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.Parameters.Add(new MySqlParameter("@date", MySqlDbType.Date));
                myCmd.Parameters.Add(new MySqlParameter("@comment", MySqlDbType.Text));
                myCmd.Parameters.Add(new MySqlParameter("@duration", MySqlDbType.Int32));
                myCmd.Parameters.Add(new MySqlParameter("@userid", MySqlDbType.Int32));
                myCmd.Parameters.Add(new MySqlParameter("@taskid", MySqlDbType.Int32));


                myCmd.Parameters["@date"].Value = record.Date;
                myCmd.Parameters["@comment"].Value = record.Comment;
                myCmd.Parameters["@duration"].Value = record.Duration;
                myCmd.Parameters["@userid"].Value = userid;
                myCmd.Parameters["@taskid"].Value = taskid;


                return Convert.ToInt32(myCmd.ExecuteScalar());
            }
        }

        public List<Record> GetAllRecords()
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {
                List<Record> records = new List<Record>();

                DataTable dt = new DataTable();
                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("szakdoga.GetAllRecords", connection);
                myCmd.CommandType = CommandType.StoredProcedure;


                MySqlDataReader sdr = myCmd.ExecuteReader();

                dt.Load(sdr);

                foreach (DataRow dr in dt.Rows)
                {
                    Record record = new Record();
                    record.IdRecord = int.Parse(dr["idRecord"].ToString());
                    record.Date = DateTime.Parse(dr["Date"].ToString());
                    record.Comment = dr["Comment"].ToString();
                    record.Duration = int.Parse(dr["Duration"].ToString());
                    record.User_idUser = int.Parse(dr["User_idUser"].ToString());
                    record.User_Username = dr["Username"].ToString();
                    record.Task_idTask = int.Parse(dr["Task_idTask"].ToString());
                    record.Task_Title = dr["Task_Title"].ToString();
                    record.Task_Status = (TaskStatus)Enum.Parse(typeof(TaskStatus), dr["Task_Status"].ToString());

                    records.Add(record);
                }

                return records;
            }
        }

        public List<Record> GetUserRecords(int userid)
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {
                List<Record> records = new List<Record>();

                DataTable dt = new DataTable();
                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("szakdoga.GetUserRecords", connection);
                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.Parameters.Add(new MySqlParameter("@userid", MySqlDbType.Int32));
                myCmd.Parameters["@userid"].Value = userid;

                MySqlDataReader sdr = myCmd.ExecuteReader();

                dt.Load(sdr);

                foreach (DataRow dr in dt.Rows)
                {
                    Record record = new Record();
                    record.IdRecord = int.Parse(dr["idRecord"].ToString());

                    //record.Date = DateTime.Parse(dr["Date"].ToShortDateString());
                    //record.Date = DateTime.Parse(dr["Date"].ToString("yyyy-MM-dd"));
                    //record.Date = DateTime.Parse(dr["Date"].ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));


                    record.Date = DateTime.Parse(dr["Date"].ToString());
                    record.Comment = dr["Comment"].ToString();
                    record.Duration = int.Parse(dr["Duration"].ToString());
                    record.DurationFormatUserProfile = TimeSpan.FromMinutes(record.Duration).ToString("hh':'mm"); //UserPorfile miatt
                    record.User_idUser = int.Parse(dr["User_idUser"].ToString());
                    record.User_Username = dr["Username"].ToString();
                    record.Task_idTask = int.Parse(dr["Task_idTask"].ToString());
                    record.Task_Title = dr["Task_Title"].ToString();
                    record.Task_Status = (TaskStatus)Enum.Parse(typeof(TaskStatus), dr["Task_Status"].ToString());

                    records.Add(record);
                }

                return records;
            }
        }

        public List<Record> GetTaskRecords(int taskid)
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {
                List<Record> records = new List<Record>();

                DataTable dt = new DataTable();
                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("szakdoga.GetTaskRecords", connection);
                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.Parameters.Add(new MySqlParameter("@taskid", MySqlDbType.Int32));
                myCmd.Parameters["@taskid"].Value = taskid;

                MySqlDataReader sdr = myCmd.ExecuteReader();

                dt.Load(sdr);

                foreach (DataRow dr in dt.Rows)
                {
                    Record record = new Record();
                    record.IdRecord = int.Parse(dr["idRecord"].ToString());
                    record.Date = DateTime.Parse(dr["Date"].ToString());
                    record.Comment = dr["Comment"].ToString();
                    record.Duration = int.Parse(dr["Duration"].ToString());
                    //record.DurationFormatUserProfile = TimeSpan.FromMinutes(record.Duration).ToString("hh':'mm"); //UserPorfile miatt
                    record.User_idUser = int.Parse(dr["User_idUser"].ToString());
                    record.User_Username = dr["Username"].ToString();
                    record.Task_idTask = int.Parse(dr["Task_idTask"].ToString());
                    record.Task_Title = dr["Task_Title"].ToString();
                    record.Task_Status = (TaskStatus)Enum.Parse(typeof(TaskStatus), dr["Task_Status"].ToString());

                    records.Add(record);
                }

                return records;
            }
        }

        public void DeleteRecord(int recordid)
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {
                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("szakdoga.DeleteRecord", connection);
                myCmd.Parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32));
                myCmd.Parameters["@id"].Value = recordid;
                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.ExecuteNonQuery();
            }
        }

        public void UpdateRecord(Record record, int recordid, int userid, int taskid)
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {
                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("szakdoga.UpdateRecord", connection);

                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.Parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32));
                myCmd.Parameters.Add(new MySqlParameter("@date", MySqlDbType.Date));
                myCmd.Parameters.Add(new MySqlParameter("@comment", MySqlDbType.Text));
                myCmd.Parameters.Add(new MySqlParameter("@duration", MySqlDbType.Int32));
                myCmd.Parameters.Add(new MySqlParameter("@userid", MySqlDbType.Int32));
                myCmd.Parameters.Add(new MySqlParameter("@taskid", MySqlDbType.Int32));

                myCmd.Parameters["@id"].Value = recordid;
                myCmd.Parameters["@date"].Value = record.Date;
                myCmd.Parameters["@comment"].Value = record.Comment;
                myCmd.Parameters["@duration"].Value = record.Duration;
                myCmd.Parameters["@userid"].Value = userid;
                myCmd.Parameters["@taskid"].Value = taskid;

                myCmd.ExecuteNonQuery();
            }
        }
    }
}