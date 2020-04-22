using GTL_Application;
using GTL_Application.Model;
using GTL_Application.ViewModel;
using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using Xunit;

namespace GTL_Test
{

    public class ViewModelTests : IDisposable
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
        // ~ViewModelTests()
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

        readonly LibraryItemDataAccess libraryItemDataAccess;
        readonly MainWindowViewModel mainWindowViewModel;
        readonly LibraryItemsListViewModel libraryItemsListViewModel;
        public ViewModelTests()
        {
            libraryItemDataAccess = new LibraryItemDataAccess();
            mainWindowViewModel = new MainWindowViewModel();
            libraryItemsListViewModel = new LibraryItemsListViewModel();
        }

        [Fact]
        public void GetLibraryItemsListCommandTest_Passes()
        {
            libraryItemsListViewModel.GetLibraryItemsListCommand.Execute(null);
            Assert.NotEmpty(libraryItemsListViewModel.LibraryItems);
        }

        [Fact]
        public void GetLibraryItemsListTest_Passes()
        {
            libraryItemsListViewModel.GetLibraryItemsList();
            Assert.NotEmpty(libraryItemsListViewModel.LibraryItems);
        }
    }
}
