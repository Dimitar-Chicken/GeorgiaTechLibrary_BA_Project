using GTL_Application.Interfaces;
using GTL_Application.Model;
using GTL_Application.Services;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;

namespace GTL_Application.ViewModel
{
    public class BorrowedItemsListViewModel : MainWindowViewModel, IBorrowedItemsListViewModel
    {
        private string _searchText;
        protected readonly IDataAccess _dataAccess;
        private ObservableCollection<ILibraryItemBorrow> _libraryItemBorrows;
        private ObservableCollection<ILibraryItemBorrow> _filtered;
        private ICommand _getLibraryItemBorrowsListCommand;
        private ICommand _getFilteredLibraryItemBorrowsListCommand;

        public BorrowedItemsListViewModel(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            InitializeAll();
        }

        public void InitializeAll()
        {
            _libraryItemBorrows = new ObservableCollection<ILibraryItemBorrow>();
            _filtered = new ObservableCollection<ILibraryItemBorrow>();
            GetLibraryItemBorrowsList();
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
            }
        }

        public ObservableCollection<ILibraryItemBorrow> FilteredLibraryItemBorrows
        {
            get { return _filtered; }
            set { SetProperty(ref _filtered, value); }
        }

        public ICommand GetLibraryItemBorrowsListCommand
        {
            get
            {
                return _getLibraryItemBorrowsListCommand ?? (_getLibraryItemBorrowsListCommand = new CommandHandler(() => GetLibraryItemBorrowsList(), () => true));
            }
        }

        public ICommand GetFilteredLibraryItemBorrowsListCommand
        {
            get
            {
                return _getFilteredLibraryItemBorrowsListCommand ?? (_getFilteredLibraryItemBorrowsListCommand = new CommandHandler(() => GetFilteredLibraryItemBorrowsList(), () => true));
            }
        }

        public void GetLibraryItemBorrowsList()
        {
            _libraryItemBorrows = _dataAccess.GetLibraryItemBorrowsList();
            if (string.IsNullOrEmpty(SearchText))
            {
                FilteredLibraryItemBorrows = _dataAccess.GetLibraryItemBorrowsList();
            }
        }

        public void GetFilteredLibraryItemBorrowsList()
        {
            FilteredLibraryItemBorrows = FilterList();
        }

        public ObservableCollection<ILibraryItemBorrow> FilterList()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                return _libraryItemBorrows;
            }
            else
            {
                _filtered.Clear();
                foreach (LibraryItemBorrow item in _libraryItemBorrows)
                {
                    // Gather a list of all the properties of the LibraryItem object instance.
                    PropertyInfo[] props = item.GetType().GetProperties();
                    // Iterate over the individual properties and retrieve the values using the Get methods.
                    foreach (var p in props)
                    {
                        var val = p.GetValue(item);
                        if (val == null)
                            return _libraryItemBorrows;

                        // If the property contains the SearchText string, set the FilterEventArgs Accepted flag to true in order to display it in the Collection.
                        if (val.ToString().ToUpper().Contains(SearchText.ToUpper()))
                            _filtered.Add(item);
                    }
                }

                return _filtered;
            }
        }
    }
}
