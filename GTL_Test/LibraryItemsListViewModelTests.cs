using GTL_Application;
using GTL_Application.Interfaces;
using GTL_Application.Model;
using GTL_Application.ViewModel;
using GTL_Test.Mocks;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Xunit;

namespace GTL_Test
{
    [ExcludeFromCodeCoverage]
    public class LibraryItemsListViewModelTests : IDisposable
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
        readonly LibraryItemsListViewModel libraryItemsListViewModel;
        public LibraryItemsListViewModelTests()
        {
            mockDataAccess = new MockDataAccess();
            libraryItemsListViewModel = new LibraryItemsListViewModel(mockDataAccess);
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

        [Theory]
        [InlineData("Tanek")]
        [InlineData("John")]
        public void TC003_LibraryItemsListViewModel_FilterList_Author_Passes(string searchText)
        {
            ObservableCollection<ILibraryItem> result;

            libraryItemsListViewModel.SearchText = searchText;

            result = libraryItemsListViewModel.FilterList();

            Assert.Contains(searchText, result[0].Author);
        }

        [Theory]
        [InlineData("Lies")]
        [InlineData("Truths")]
        public void TC003_LibraryItemsListViewModel_FilterList_Title_Passes(string searchText)
        {
            ObservableCollection<ILibraryItem> result;

            libraryItemsListViewModel.SearchText = searchText;

            result = libraryItemsListViewModel.FilterList();

            Assert.Contains(searchText, result[0].Title);
        }
    }
}
