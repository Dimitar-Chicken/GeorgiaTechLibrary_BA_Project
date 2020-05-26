using GTL_Application.Model;
using GTL_Application.ViewModel;
using GTL_Test.Mocks;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security;
using System.Text;
using System.Windows.Controls;
using Xunit;

namespace GTL_Test
{
    [ExcludeFromCodeCoverage]
    public class NewBorrowItemEntryViewModelTests
    {
        readonly MockDataAccess mockDataAccess;
        readonly NewBorrowedItemEntryViewModel newBorrowedItemEntryViewModel;
        public NewBorrowItemEntryViewModelTests()
        {
            mockDataAccess = new MockDataAccess();
            newBorrowedItemEntryViewModel = new NewBorrowedItemEntryViewModel(mockDataAccess);
        }

        [Fact]
        public void TC007_NewBorrowedItemEntryViewModel_GetBorrowableBookCopiesListCommand_Passes()
        {
            newBorrowedItemEntryViewModel.GetBorrowableBookCopiesListCommand.Execute(null);
            
            Assert.NotEmpty(newBorrowedItemEntryViewModel.BorrowableBookCopiesList);
        }
    }
}
