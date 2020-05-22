using GTL_Application.Interfaces;
using GTL_Application.ViewModel;
using GTL_Test.Mocks;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
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
        [InlineData("Lies")]
        [InlineData("Truths")]
        [InlineData("Book")]
        [InlineData("dummy text")]
        [InlineData("Subject")]
        public void TC003_LibraryItemsListViewModel_FilterList_Passes(string searchText)
        {
            ObservableCollection<ILibraryItem> result;

            libraryItemsListViewModel.SearchText = searchText;

            result = libraryItemsListViewModel.FilterList();

            Assert.Contains(result[0].GetType().GetProperties(), p => p.GetValue(result[0]).ToString().Contains(searchText));
        }
    }
}
