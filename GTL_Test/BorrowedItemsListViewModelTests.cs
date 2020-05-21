using GTL_Application.Interfaces;
using GTL_Application.ViewModel;
using GTL_Test.Mocks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Xunit;

namespace GTL_Test
{
    [ExcludeFromCodeCoverage]
    public class BorrowedItemsListViewModelTests : DisposableImplementation
    {
        readonly MockDataAccess mockDataAccess;
        readonly BorrowedItemsListViewModel borrowedItemsListViewModel;

        public BorrowedItemsListViewModelTests()
        {
            mockDataAccess = new MockDataAccess();
            borrowedItemsListViewModel = new BorrowedItemsListViewModel(mockDataAccess);
        }

        [Fact]
        public void TC002_BorrowedItemsListViewModel_GetLibraryItemBorrowsListCommand_Passes()
        {
            borrowedItemsListViewModel.GetLibraryItemBorrowsListCommand.Execute(null);
            Assert.NotEmpty(borrowedItemsListViewModel.FilteredLibraryItemBorrows);
        }

        [Fact]
        public void TC002_BorrowedItemsListViewModel_GetFilteredLibraryItemBorrowsListCommand_Passes()
        {
            borrowedItemsListViewModel.GetFilteredLibraryItemBorrowsListCommand.Execute(null);
            Assert.NotEmpty(borrowedItemsListViewModel.FilteredLibraryItemBorrows);
        }

        [Theory]
        [InlineData("Fredrik")]
        [InlineData("Streisand")]
        public void TC004_BorrowedItemsListViewModel_FilterList_PersonName_Passes(string searchText)
        {
            ObservableCollection<ILibraryItemBorrow> result;

            borrowedItemsListViewModel.SearchText = searchText;

            result = borrowedItemsListViewModel.FilterList();

            Assert.Contains(searchText, result[0].PersonName);
        }

        [Theory]
        [InlineData("9783221484100")]
        [InlineData("9783161484400")]
        public void TC004_BorrowedItemsListViewModel_FilterList_ISBN_Passes(string searchText)
        {
            ObservableCollection<ILibraryItemBorrow> result;

            borrowedItemsListViewModel.SearchText = searchText;

            result = borrowedItemsListViewModel.FilterList();

            Assert.Contains(searchText, result[0].ISBN);
        }

    }
}
