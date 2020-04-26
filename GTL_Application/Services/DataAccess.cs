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

            string query = @"SELECT LibraryItems.LibraryItem.Title, LibraryItems.Author.AuthorName, LibraryItems.LibraryItem.SubjectArea, LibraryItems.LibraryItem.ItemDescription, LibraryItems.LibraryItemType.TypeName AS LibraryItemType " + 
                            "FROM LibraryItems.LibraryItem " +
                            "INNER JOIN LibraryItems.Author ON LibraryItems.Author.AuthorID = LibraryItems.LibraryItem.AuthorID " +
                            "INNER JOIN LibraryItems.LibraryItemType ON LibraryItems.LibraryItemType.TypeID = LibraryItems.LibraryItem.LibraryItemTypeID ";
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

            string query = @"SELECT LibraryItems.ISBN.ISBN, LibraryItems.LibraryItem.Title, CONCAT(People.Person.FirstName, ' ', People.Person.LastName) AS PersonName, BookBorrow.Borrows.BorrowDate, BookBorrow.Borrows.ReturnDate " +
                            "FROM BookBorrow.Borrows " +
                            "INNER JOIN People.Person ON BookBorrow.Borrows.PersonSSN = People.Person.SSN " +
                            "INNER JOIN LibraryItems.ISBN ON BookBorrow.Borrows.ISBNID = LibraryItems.ISBN.ISBNID " +
                            "INNER JOIN LibraryItems.LibraryItem ON LibraryItems.ISBN.LibraryItemID = LibraryItems.LibraryItem.LibraryItemID ";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                LibraryItemBorrow libraryItemBorrow = new LibraryItemBorrow
                {
                    PersonName = dataReader["PersonName"].ToString(),
                    Title = dataReader["Title"].ToString(),
                    ISBN = dataReader["ISBN"].ToString(),
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
