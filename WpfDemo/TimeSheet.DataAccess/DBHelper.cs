using System.Configuration;

namespace TimeSheet.DataAccess
{
    public static class DBHelper
    {
        private const string ConnectionStringKey = "demofeladatDB";

        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[ConnectionStringKey].ConnectionString;
        }
    }
}
