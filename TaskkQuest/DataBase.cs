using System;
using System.Data.SqlClient;

public class Database
{

    private static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TaskQuestDB;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;";

    public static SqlConnection GetConnection()
    {
        return new SqlConnection(connectionString);
    }

    public static bool TestarLigacao() 
    {
        try
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                return true;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }
}