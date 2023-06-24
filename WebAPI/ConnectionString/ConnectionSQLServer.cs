

using System.Data.SqlClient;

namespace WebAPI.ConnectionString
{
    public class ConnectionSQLServer
    {
        public static string ConnectionString = "Server=LABSAPB192P5;Initial Catalog=DB_KOFI_API_TESTING_06-09-2021_1;Persist Security Info=False;User ID=sa;Password=SAPB1Admin;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;";
           // "Data Source=LABSAPB192P5;Initial Catalog=DB_KOFI_API_TESTING_06-09-2021_1;User Id=sa;Password=SAPB1Admin;TrustServerCertificate=False";
           //Data Source=LABSAPB192P5;Initial Catalog=DB_KOFI_API_TESTING_06-09-2021;User Id=sa;Password=SAPB1Admin
        public static SqlConnection Connection()
        {
            SqlConnection sQlConnection = new SqlConnection(ConnectionString);
            return sQlConnection;
        }
    }
}
