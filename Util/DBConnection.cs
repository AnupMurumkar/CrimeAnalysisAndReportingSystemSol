using System;
using System.Data.SqlClient;

namespace CrimeAnalysisAndReportingSystemSol.Util
{
    public class DBConnection
    {
        private static SqlConnection connection;

        // Connection string (as provided)
        private static readonly string connectionString = "Data Source=LAPTOP-8JT18MT0;Initial Catalog=CrimeReportingSystem;Integrated Security=True;Trust Server Certificate=True";

        // Static method to return a SQL connection object
        public static SqlConnection GetConnection()
        {
            if (connection == null || connection.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    // Initialize SQL Connection using the provided connection string
                    connection = new SqlConnection(connectionString);
                    connection.Open(); // Open the connection
                }
                catch (Exception ex)
                {
                    throw new Exception("Error establishing database connection: " + ex.Message);
                }
            }

            return connection;
        }
    }
}
