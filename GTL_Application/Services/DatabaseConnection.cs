using System;
using System.Data.SqlClient;

namespace GTL_Application
{
    public class DatabaseConnection
    {
        string connectionString;
        SqlConnection connection;

        public DatabaseConnection()
        {
            connectionString = @"Server=ANOOBIS-DESKTOP\SQL2019; Database=GeorgiaTechLibrary_BA_Project_DB; User Id=sa; Password=1234;";

            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public void OpenConnection()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();

        }

        public string Read()
        {
            //DOESN'T WORK, FIND OUT WHY
            string result = String.Empty;
            string query = "SELECT * FROM Member";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                result = dr["MemberSSN"].ToString();
            }
            return result;
        }
    }
}
