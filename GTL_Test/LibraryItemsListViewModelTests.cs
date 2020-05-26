using GalaSoft.MvvmLight.Messaging;
using GTL_Application.Interfaces;
using GTL_Application.Services;
using GTL_Application.ViewModel;
using GTL_Test.Helpers;
using GTL_Test.Mocks;
using System.Collections.ObjectModel;
using System.Configuration;
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

            libraryItemsListViewModel.GetFilteredLibraryItemsList();
            result = libraryItemsListViewModel.FilteredLibraryItems;

            Assert.Contains(result[0].GetType().GetProperties(), p => p.GetValue(result[0]).ToString().Contains(searchText));
        }

        [Fact]
        public void TC005_LibraryItemsListViewModel_OpenDescriptionWindowCommand_Passes()
        {
            string description = "test";
            string result = string.Empty;

            Messenger.Default.Register<ViewModelCarrier<string>>(this, (action) =>
            {
                result = action.viewModel;
            });

            libraryItemsListViewModel.OpenDescriptionWindowCommand.Execute(description);

            Assert.Equal(description, result);
        }

        [Theory]
        [InlineData("Test")]
        [InlineData("Asdasdasdasd")]
        [InlineData("111111")]
        [InlineData("")]
        public void TC005_LibraryItemsListVIewModel_OpenItemDescriptionWindow_Passes(string description)
        {
            string result = string.Empty;
            
            Messenger.Default.Register<ViewModelCarrier<string>>(this, (action) =>
            {
                result = action.viewModel;
            });

            libraryItemsListViewModel.OpenItemDescriptionWindow(description);

            Assert.Equal(description, result);
        }
    }
}
