using System.Data.SqlClient;

namespace TaskQuest
{
    public class DatabaseHelper
    {
       
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TaskQuestDB;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;";


        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

   
        public bool TestarConexao()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}