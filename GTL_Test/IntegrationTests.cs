using GTL_Application.Services;
using GTL_Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
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
            dataAccess = new DataAccess();
            libraryItemsListViewModel = new LibraryItemsListViewModel(dataAccess);
            borrowedItemsListViewModel = new BorrowedItemsListViewModel(dataAccess);
        }

        [Fact]
        public void TC001_LibraryItemsListViewModel_GetLibraryItemsListCommand_Passes()
        {
            libraryItemsListViewModel.GetLibraryItemsListCommand.Execute(null);
            Assert.NotEmpty(libraryItemsListViewModel.FilteredLibraryItems);
        }

        [Fact]
        public void TC001_LibraryItemsListViewModel_GetFilteredLibraryItemsListCommand_Passes()
        {
            libraryItemsListViewModel.GetFilteredLibraryItemsListCommand.Execute(null);
            Assert.NotEmpty(libraryItemsListViewModel.FilteredLibraryItems);
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
    }
}
