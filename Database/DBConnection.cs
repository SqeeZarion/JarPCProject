namespace JarPControlProject.Database;
using System.Data.SqlClient;

using System.Data.SqlClient;

public static class DBConnection 
{
    
    private const string SERVER_NAME = "SQEEZES\\MSSQLSERVER1";
        private const string DATABASE_NAME = "JarPCProject";
        private const string USER_ID = "SqeeZarion";
        private const string PASSWORD = "mopsik_tawerka09";
        
        private const string CONNECTION_STRING = "Data Source=" + SERVER_NAME + ";Initial Catalog=" + DATABASE_NAME + ";User ID=" + USER_ID + ";Password=" + PASSWORD;

        
        public static SqlConnection GetConnection() 
        {
            SqlConnection conn = new SqlConnection(CONNECTION_STRING);
            return conn;
        }
}
