using GTL_Application.Interfaces;
using GTL_Application.Model;
using GTL_Application.Services;
using GTL_Application.ViewModel;
using GTL_Test.Helpers;
using GTL_Test.Mocks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;

namespace GTL_Test
{
    [ExcludeFromCodeCoverage]
    public class IntegrationTests : DisposableImplementation
    {
        readonly DataAccess dataAccess;
        readonly LibraryItemsListViewModel libraryItemsListViewModel;
        readonly BorrowedItemsListViewModel borrowedItemsListViewModel;

        public IntegrationTests ()
        {
            string path = @"C:\Users\anoobis\OneDrive - Professionshøjskolen UCN\Uni_stuff\6th_semester\0_SEMESTER_REPORT\DATABASE\GTLSetup.sql";

            string connectionString = @"Server=ANOOBIS-DESKTOP\SQL2019;" +
                                          "Database=GeorgiaTechLibrary_BA_Project_DB;" +
                                          "User Id=sa;" +
                                          "Password=1234;";

            DatabaseReset serverReset = new DatabaseReset();
            serverReset.runSqlScriptFile(path, connectionString);
            dataAccess = new DataAccess();
            libraryItemsListViewModel = new LibraryItemsListViewModel(dataAccess);
            borrowedItemsListViewModel = new BorrowedItemsListViewModel(dataAccess);
        }

        [Fact]
        public void TC001_LibraryItemsListViewModel_GetFilteredLibraryItemsListCommand_Passes()
        {
            libraryItemsListViewModel.GetFilteredLibraryItemsListCommand.Execute(null);

            Assert.NotEmpty(libraryItemsListViewModel.FilteredLibraryItems);
        }

        [Fact]
        public void TC002_BorrowedItemsListViewModel_GetFilteredLibraryItemBorrowsListCommand_Passes()
        {
            borrowedItemsListViewModel.GetFilteredLibraryItemBorrowsListCommand.Execute(null);

            Assert.NotEmpty(borrowedItemsListViewModel.FilteredLibraryItemBorrows);
        }

        [Fact]
        public void TC008_DataAccess_GetLibraryItemList_Passes()
        {
            ObservableCollection<ILibraryItem> result;

            result = dataAccess.GetLibraryItemList();

            Assert.NotEmpty(result);
        }

        [Fact]
        public void TC008_DataAccess_GetLibraryItemBorrowsList_Passes()
        {
            ObservableCollection<ILibraryItemBorrow> result;

            result = dataAccess.GetLibraryItemBorrowsList();

            Assert.NotEmpty(result);
        }

        [Fact]
        public void TC008_DataAccess_GetBorrowableBookCopiesList_Passes()
        {
            ObservableCollection<IBorrowableBookCopy> result;

            result = dataAccess.GetBorrowableBookCopiesList();

            Assert.NotEmpty(result);
        }

        [Fact]
        public void TC008_DataAccess_GetPeople_Passes()
        {
            ObservableCollection<IPerson> result;

            result = dataAccess.GetPeople();

            Assert.NotEmpty(result);
        }

        [Fact]
        public void TC011_DataAccess_CreateNewBookBorrow_Passes()
        {
            bool result;

            SecureString SSN = new NetworkCredential("", "16110414-2128").SecurePassword;
            IBorrowableBookCopy borrowableBookCopy = new BorrowableBookCopy() { ID = new NetworkCredential("", "51").SecurePassword };

            result = dataAccess.CreateNewBookBorrow(SSN, borrowableBookCopy);

            Assert.True(result);
        }
    }
}
