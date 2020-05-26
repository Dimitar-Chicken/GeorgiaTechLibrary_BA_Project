using GalaSoft.MvvmLight.Messaging;
using GTL_Application.Interfaces;
using GTL_Application.Services;
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
    public class PeopleListViewModelTests
    {
        readonly MockDataAccess mockDataAccess;
        readonly PeopleListViewModel peopleListViewModel;
        public PeopleListViewModelTests()
        {
            mockDataAccess = new MockDataAccess();
            peopleListViewModel = new PeopleListViewModel(mockDataAccess);
        }

        [Fact]
        public void TC014_PeopleListViewModel_GetFilteredPeopleListCommand_Passes()
        {
            peopleListViewModel.GetFilteredPeopleListCommand.Execute(null);

            Assert.NotEmpty(peopleListViewModel.FilteredPeopleList);
        }

        [Theory]
        [InlineData("John")]
        [InlineData("Boe")]
        [InlineData("1234567890")]
        [InlineData("email2@example.com")]
        public void TC014_PeopleListViewModel_FilterList_Passes(string searchText)
        {
            ObservableCollection<IPerson> result;

            peopleListViewModel.SearchText = searchText;

            peopleListViewModel.GetFilteredPeopleList();
            result = peopleListViewModel.FilteredPeopleList;

            Assert.Contains(result[0].GetType().GetProperties(), p => p.GetValue(result[0]).ToString().Contains(searchText));
        }

        [Fact]
        public void TC015_PeopleListViewModel_OpenMembershipWindow()
        {
            object[] result = null;
            object[] dates = new object[2] { DateTime.Now, DateTime.Now };
            
            Messenger.Default.Register<ViewModelCarrier<object[]>>(this, (action) =>
            {
                result = action.viewModel;
            });
            peopleListViewModel.OpenMembershipWindowCommand.Execute(dates);

            Assert.Same(dates, result);
        }
    }
}
