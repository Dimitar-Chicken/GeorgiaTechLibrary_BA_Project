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
    public class BorrowedItemsListViewModelTests : IDisposable
    {
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~LibraryItemsListViewModelTests()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
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
