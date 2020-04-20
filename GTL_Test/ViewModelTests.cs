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
        bool disposed = false;
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        MainWindowViewModel mainWindowViewModel;
        public ViewModelTests()
        {
            mainWindowViewModel = new MainWindowViewModel();
        }
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if (disposing)
            {
                handle.Dispose();
            }

            disposed = true;
        }

        [Fact]
        public void GetLibraryItemsListCommandTest_Passes()
        {
            mainWindowViewModel.GetLibraryItemsListCommand.Execute(null);
            Assert.NotEmpty(mainWindowViewModel.LibraryItems);
        }

        [Fact]
        public void GetLibraryItemsListTest_Passes()
        {
            mainWindowViewModel.GetLibraryItemsList();
            Assert.NotEmpty(mainWindowViewModel.LibraryItems);
        }
    }
}
