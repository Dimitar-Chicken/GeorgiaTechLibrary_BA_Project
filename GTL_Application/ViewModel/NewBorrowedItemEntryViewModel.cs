using GTL_Application.Interfaces;
using GTL_Application.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace GTL_Application.ViewModel
{
    public class NewBorrowedItemEntryViewModel : MainWindowViewModel, INewBorrowedItemEntryViewModel
    {
        private IDataAccess _dataAccess;
        private ICommand _getBorrowableBookCopiesCommand;
        private ICommand _createNewLibraryItemBorrowEntryCommand;
        private bool _result;
        private IBorrowableBookCopy _selectedBorrowableBookCopy;
        private ObservableCollection<IBorrowableBookCopy> _borrowableBookCopiesList;
        public NewBorrowedItemEntryViewModel(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            _borrowableBookCopiesList = new ObservableCollection<IBorrowableBookCopy>();
            GetBorrowableBookCopiesList();
        }

        public bool Result
        {
            get
            {
                return _result;
            }
            set
            {
                SetProperty(ref _result, value);
            }
        }

        public IBorrowableBookCopy SelectedBorrowableBookCopy
        {
            get
            {
                return _selectedBorrowableBookCopy;
            }
            set
            {
                SetProperty(ref _selectedBorrowableBookCopy, value);
            }
        }

        public ObservableCollection<IBorrowableBookCopy> BorrowableBookCopiesList
        {
            get
            {
                return _borrowableBookCopiesList;
            }
            set
            {
                SetProperty(ref _borrowableBookCopiesList, value);
            }
        }

        public ICommand GetBorrowableBookCopiesListCommand
        {
            get
            {
                return _getBorrowableBookCopiesCommand ?? (_getBorrowableBookCopiesCommand = new CommandHandler(() => GetBorrowableBookCopiesList(), () => true));
            }
        }

        public ICommand CreateNewLibraryItemBorrowEntryCommand
        {
            get
            {
                return _createNewLibraryItemBorrowEntryCommand ?? (_createNewLibraryItemBorrowEntryCommand = new CommandHandlerWithParameters<object>((input) => CreateNewLibraryItemBorrowEntry(input), () => true));
            }
        }

        public void GetBorrowableBookCopiesList()
        {
            BorrowableBookCopiesList = _dataAccess.GetBorrowableBookCopiesList();
        }

        public void CreateNewLibraryItemBorrowEntry(object input)
        {
            PasswordBox passwordBox = input as PasswordBox;
            Result = _dataAccess.CreateNewBookBorrow(passwordBox.SecurePassword, SelectedBorrowableBookCopy);
        }
    }
}
