using GTL_Application.Model;
using GTL_Application.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;

namespace GTL_Application.ViewModel
{
    public class BorrowedItemsListViewModel : MainWindowViewModel
    {
        private string _searchText;
        protected readonly DataAccess _dataAccess;
        private ObservableCollection<LibraryItemBorrow> _libraryItemBorrows;
        private CollectionViewSource _libraryItemBorrowsCollection;
        private ICommand _getLibraryItemBorrowsListCommand;
        public BorrowedItemsListViewModel()
        {
            _dataAccess = new DataAccess();
            InitializeAll();
        }

        public BorrowedItemsListViewModel(DataAccess mockDataAccess)
        {
            _dataAccess = mockDataAccess;
            InitializeAll();
        }

        public void InitializeAll()
        {
            _libraryItemBorrows = new ObservableCollection<LibraryItemBorrow>();
            GetLibraryItemBorrowsList();
            _libraryItemBorrowsCollection = new CollectionViewSource
            {
                Source = LibraryItemBorrows
            };
            _libraryItemBorrowsCollection.Filter += (sender, FilterEventArgs) => { CollectionFilter(sender, FilterEventArgs, SearchText); };
        }

        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;
                _libraryItemBorrowsCollection.View.Refresh();
            }
        }

        public ICollectionView LibraryItemBorrowsCollection
        {
            get
            {
                return _libraryItemBorrowsCollection.View;
            }
        }

        public ObservableCollection<LibraryItemBorrow> LibraryItemBorrows
        {
            get { return _libraryItemBorrows; }
            set { SetProperty(ref _libraryItemBorrows, value); }
        }

        public ICommand GetLibraryItemBorrowsListCommand
        {
            get
            {
                return _getLibraryItemBorrowsListCommand ?? (_getLibraryItemBorrowsListCommand = new CommandHandler(() => GetLibraryItemBorrowsList(), () => true));
            }
        }

        public void GetLibraryItemBorrowsList()
        {
            LibraryItemBorrows = _dataAccess.GetLibraryItemBorrowsList();
        }
    }
}
