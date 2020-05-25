using GalaSoft.MvvmLight.Messaging;
using GTL_Application.Interfaces;
using GTL_Application.Services;
using GTL_Application.ViewModel;
using GTL_Test.Mocks;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
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
        [InlineData("9783221484100")]
        [InlineData("9783161484400")]
        [InlineData("On Loan")]
        [InlineData("Overdue")]
        public void TC004_BorrowedItemsListViewModel_FilterList_Passes(string searchText)
        {
            ObservableCollection<ILibraryItemBorrow> result;

            borrowedItemsListViewModel.SearchText = searchText;

            result = borrowedItemsListViewModel.FilterList();

            Assert.Contains(result[0].GetType().GetProperties(), p => p.GetValue(result[0]).ToString().Contains(searchText));
        }

        [Fact]
        public void TC006_BorrowedItemsListViewModel_OpenNewEntryWindowCommand_Passes()
        {
            INewBorrowedItemEntryViewModel result = null;

            Messenger.Default.Register<ViewModelCarrier<INewBorrowedItemEntryViewModel>>(this, (action) =>
            {
                result = action.viewModel;
            });

            borrowedItemsListViewModel.OpenNewEntryWindowCommand.Execute(null);
            Assert.IsType<NewBorrowedItemEntryViewModel>(result);
        }
    }
}
