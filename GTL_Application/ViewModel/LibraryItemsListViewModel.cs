using GTL_Application.Interfaces;
using GTL_Application.Model;
using GTL_Application.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Input;

namespace GTL_Application.ViewModel
{
    public class LibraryItemsListViewModel : MainWindowViewModel, ILibraryItemsListViewModel
    {
        private string _searchText;
        protected readonly IDataAccess _dataAccess;
        private ObservableCollection<ILibraryItem> _libraryItems;
        private ObservableCollection<ILibraryItem> _filtered;
        private ICommand _getLibraryItemsListCommand;
        private ICommand _getFilteredLibraryItemsListCommand;

        public LibraryItemsListViewModel(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            InitializeAll();
        }

        public void InitializeAll()
        {
            _libraryItems = new ObservableCollection<ILibraryItem>();
            _filtered = new ObservableCollection<ILibraryItem>();
            GetLibraryItemsList();
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

        public ObservableCollection<ILibraryItem> FilteredLibraryItems
        {
            get { return _filtered; }
            set { SetProperty(ref _filtered, value); }
        }

        public ICommand GetLibraryItemsListCommand
        {
            get
            {
                return _getLibraryItemsListCommand ?? (_getLibraryItemsListCommand = new CommandHandler(() => GetLibraryItemsList(), () => true));
            }
        }

        public ICommand GetFilteredLibraryItemsListCommand
        {
            get
            {
                return _getFilteredLibraryItemsListCommand ?? (_getFilteredLibraryItemsListCommand = new CommandHandler(() => GetFilteredLibraryItemsList(), () => true));
            }
        }

        public void GetLibraryItemsList()
        {
            _libraryItems = _dataAccess.GetLibraryItemList();
            if (string.IsNullOrEmpty(SearchText))
            {
                FilteredLibraryItems = _dataAccess.GetLibraryItemList();
            }
        }

        public void GetFilteredLibraryItemsList()
        {
            FilteredLibraryItems = FilterList();
        }

        public ObservableCollection<ILibraryItem> FilterList()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                return _libraryItems;
            }
            else
            {
                _filtered.Clear();
                foreach (ILibraryItem item in _libraryItems)
                {
                    // Gather a list of all the properties of the LibraryItem object instance.
                    PropertyInfo[] props = item.GetType().GetProperties();
                    // Iterate over the individual properties and retrieve the values using the Get methods.
                    foreach (var p in props)
                    {
                        var val = p.GetValue(item);
                        if (val == null)
                            return _libraryItems;

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
