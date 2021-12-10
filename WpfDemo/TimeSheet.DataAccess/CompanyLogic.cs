using MySql.Data.MySqlClient;
using System.Data;
using TimeSheet.Model;

namespace TimeSheet.DataAccess
{
    public class CompanyLogic : ICompanyLogic
    {
        public Company GetCompany()
        {
            using (MySqlConnection connection = new MySqlConnection(DBHelper.GetConnectionString()))
            {
                Company company = new Company();
                DataTable dt = new DataTable();
                connection.Open();

                MySqlCommand myCmd = new MySqlCommand("szakdoga.GetCompany", connection);
                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.ExecuteNonQuery();

                MySqlDataReader sdr = myCmd.ExecuteReader();

                dt.Load(sdr);

                foreach (DataRow dr in dt.Rows)
                {
                    company.IdCompany = int.Parse(dr["IdCompany"].ToString());
                    company.CompanyName = dr["CompanyName"].ToString();
                }

                return company;
            }
        }
    }
}
