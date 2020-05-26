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
        readonly NewBorrowedItemEntryViewModel newBorrowedItemEntryViewModel;
        readonly PeopleListViewModel peopleListViewModel;

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
            newBorrowedItemEntryViewModel = new NewBorrowedItemEntryViewModel(dataAccess);
            peopleListViewModel = new PeopleListViewModel(dataAccess);
        }

        [Fact]
        public void TC001_LibraryItemsListViewModel_GetFilteredLibraryItemsListCommand_Passes()
        {
            libraryItemsListViewModel.GetFilteredLibraryItemsListCommand.Execute(null);

            Assert.NotEmpty(libraryItemsListViewModel.FilteredLibraryItems);
        }

        [Theory]
        [InlineData("Tanek")]
        [InlineData("John")]
        [InlineData("Aliquam")]
        public void TC001_LibraryItemsListViewModel_GetFilteredLibraryItemsListCommand_SearchText_Passes(string searchText)
        {
            libraryItemsListViewModel.SearchText = searchText;

            libraryItemsListViewModel.GetFilteredLibraryItemsListCommand.Execute(null);

            Assert.NotEmpty(libraryItemsListViewModel.FilteredLibraryItems);
        }

        [Fact]
        public void TC002_BorrowedItemsListViewModel_GetFilteredLibraryItemBorrowsListCommand_Passes()
        {
            borrowedItemsListViewModel.GetFilteredLibraryItemBorrowsListCommand.Execute(null);

            Assert.NotEmpty(borrowedItemsListViewModel.FilteredLibraryItemBorrows);
        }

        [Theory]
        [InlineData("1649111101773")]
        [InlineData("1694052205484")]
        [InlineData("On Loan")]
        public void TC002_BorrowedItemsListViewModel_GetFilteredLibraryItemBorrowsListCommand_SearchText_Passes(string searchText)
        {
            borrowedItemsListViewModel.SearchText = searchText;

            borrowedItemsListViewModel.GetFilteredLibraryItemBorrowsListCommand.Execute(null);

            Assert.NotEmpty(borrowedItemsListViewModel.FilteredLibraryItemBorrows);
        }

        [Fact]
        public void TC007_NewBorrowedItemEntryViewModel_GetBorrowableBookCopiesListCommand_Passes()
        {
            newBorrowedItemEntryViewModel.GetBorrowableBookCopiesListCommand.Execute(null);

            Assert.NotEmpty(newBorrowedItemEntryViewModel.BorrowableBookCopiesList);
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

        [Fact]
        public void TC012_DataAccess_CreateNewBookBorrow_SQLInjection_ThrowsException()
        {
            SecureString SSN = new NetworkCredential("", "); DROP TABLE [LibraryItems].[Borrow];--").SecurePassword;
            IBorrowableBookCopy borrowableBookCopy = new BorrowableBookCopy() { ID = new NetworkCredential("", "51").SecurePassword };

            Assert.Throws<SqlException>(() => dataAccess.CreateNewBookBorrow(SSN, borrowableBookCopy));
        }

        [Fact]
        public void TC014_PeopleListViewModel_GetFilteredPeopleListCommand_Passes()
        {
            peopleListViewModel.GetFilteredPeopleListCommand.Execute(null);

            Assert.NotEmpty(peopleListViewModel.FilteredPeopleList);
        }

        [Theory]
        [InlineData("Donna")]
        [InlineData("Member")]
        [InlineData("Professor")]
        public void TC014_PeopleListViewModel_GetFilteredPeopleListCommand_SearchText_Passes(string searchText)
        {
            peopleListViewModel.SearchText = searchText;

            peopleListViewModel.GetFilteredPeopleListCommand.Execute(null);

            Assert.NotEmpty(peopleListViewModel.FilteredPeopleList);
        }
    }
}
