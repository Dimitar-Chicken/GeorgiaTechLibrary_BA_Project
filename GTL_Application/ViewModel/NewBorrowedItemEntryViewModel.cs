using GTL_Application.Interfaces;
using GTL_Application.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace GTL_Application.ViewModel
{
    public class NewBorrowedItemEntryViewModel : MainWindowViewModel, INewBorrowedItemEntryViewModel
    {
        private IDataAccess _dataAccess;
        private ICommand _getLibraryItemBorrowsListCommand;
        private ICommand _createNewLibraryItemBorrowEntryCommand;
        private ObservableCollection<ILibraryItem> _libraryItemList;
        public NewBorrowedItemEntryViewModel(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            _libraryItemList = new ObservableCollection<ILibraryItem>();
            GetLibraryItemBorrowsList();
        }

        public void GetLibraryItemBorrowsList()
        {
            _libraryItemList = _dataAccess.GetLibraryItemList();
        }

        public ObservableCollection<ILibraryItem> LibraryItemList
        {
            get
            {
                return _libraryItemList;
            }
            set
            {
                SetProperty(ref _libraryItemList, value);
            }
        }

        public ICommand GetLibraryItemBorrowsListCommand
        {
            get
            {
                return _getLibraryItemBorrowsListCommand ?? (_getLibraryItemBorrowsListCommand = new CommandHandler(() => GetLibraryItemBorrowsList(), () => true));
            }
        }

        public ICommand CreateNewLibraryItemBorrowEntryCommand
        {
            get
            {
                return _createNewLibraryItemBorrowEntryCommand ?? (_createNewLibraryItemBorrowEntryCommand = new CommandHandler(() => CreateNewLibraryItemBorrowEntry(), () => true));
            }
        }

        public void CreateNewLibraryItemBorrowEntry()
        {
            throw new NotImplementedException();
        }
    }
}
