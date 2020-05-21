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
    public class LibraryItemsListViewModelTests : DisposableImplementation
    {
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
