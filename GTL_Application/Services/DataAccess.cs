using GTL_Application.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace GTL_Application
{
    public class DataAccess
    {
        private string connectionString;

        public DataAccess()
        {
            connectionString = @"Server=ANOOBIS-DESKTOP\SQL2019;" +
                                "Database=GeorgiaTechLibrary_BA_Project_DB;" +
                                "User Id=sa;" +
                                "Password=1234;";
        }

        public ObservableCollection<LibraryItem> GetLibraryItemList()
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            ObservableCollection<LibraryItem> libraryItems = new ObservableCollection<LibraryItem>();
            connection.Open();

            string query = @"SELECT * FROM GetLibraryItems";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                LibraryItem libraryItem = new LibraryItem
                {
                    Title = dataReader["Title"].ToString(),
                    Author = dataReader["AuthorName"].ToString(),
                    SubjectArea = dataReader["SubjectArea"].ToString(),
                    ItemDescription = dataReader["ItemDescription"].ToString(),
                    TypeName = dataReader["LibraryItemType"].ToString()
                };

                libraryItems.Add(libraryItem);
            }
            connection.Close();

            return libraryItems;
        }

        public ObservableCollection<LibraryItemBorrow> GetLibraryItemBorrowsList()
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            ObservableCollection<LibraryItemBorrow> libraryItemBorrows = new ObservableCollection<LibraryItemBorrow>();
            connection.Open();

            string query = @"SELECT * FROM GetBookBorrows";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                LibraryItemBorrow libraryItemBorrow = new LibraryItemBorrow
                {
                    PersonName = dataReader["PersonName"].ToString(),
                    Title = dataReader["Title"].ToString(),
                    ISBN = dataReader["ISBN"].ToString(),
                    Status = dataReader["BookStatus"].ToString(),
                    //TODO: Add check for Parsing success.
                    BorrowDate = Convert.ToDateTime(dataReader["BorrowDate"]),
                    ReturnDate = Convert.ToDateTime(dataReader["ReturnDate"])
                };

                libraryItemBorrows.Add(libraryItemBorrow);
            }
            connection.Close();


            return libraryItemBorrows;
        }
    }
}
