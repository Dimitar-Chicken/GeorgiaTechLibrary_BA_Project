﻿using GTL_Application.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace GTL_Application
{
    public class LibraryItemDataAccess
    {
        private string connectionString;

        public LibraryItemDataAccess()
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


            string query = "SELECT * FROM LibraryItems.LibraryItem";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                LibraryItem libraryItem = new LibraryItem
                {
                    Title = dataReader["Title"].ToString(),
                    Author = dataReader["Author"].ToString(),
                    SubjectArea = dataReader["SubjectArea"].ToString(),
                    ItemDescription = dataReader["ItemDescription"].ToString()
                    // TypeName = dataReader["LibraryItemType"].ToString();
                };

                libraryItems.Add(libraryItem);
            }
            connection.Close();

            return libraryItems;
        }
    }
}
