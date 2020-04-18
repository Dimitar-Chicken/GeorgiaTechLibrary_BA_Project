using GTL_Application.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GTL_Application
{
    public class LibraryItemDataAccess
    {
        string connectionString;
        SqlConnection connection;

        public LibraryItemDataAccess()
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

        public List<LibraryItem> GetLibraryItemList()
        {
            List<LibraryItem> libraryItems = new List<LibraryItem>();

            connection = new SqlConnection(connectionString);
            connection.Open();


            string query = "SELECT * FROM LibraryItems.LibraryItem";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                LibraryItem libraryItem = new LibraryItem();
                
                libraryItem.Title = dataReader["Title"].ToString();
                libraryItem.Author = dataReader["Author"].ToString();
                libraryItem.SubjectArea = dataReader["SubjectArea"].ToString();
                libraryItem.ItemDescription = dataReader["ItemDescription"].ToString();
                // libraryItem.TypeName = dataReader["LibraryItemType"].ToString();

                libraryItems.Add(libraryItem);
            }

            return libraryItems;
        }
    }
}
