using GTL_Application.Model;
using GTL_Application.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Input;

namespace GTL_Application.ViewModel
{
    public class LibraryItemsListViewModel : MainWindowViewModel
    {
        private string _searchText;
        protected readonly DataAccess _dataAccess;
        private ObservableCollection<LibraryItem> _libraryItems;
        private CollectionViewSource _libraryItemsCollection;
        private ICommand _getLibraryItemsListCommand;

        public LibraryItemsListViewModel()
        {
            _dataAccess = new DataAccess();
            InitializeAll();
        }

        public LibraryItemsListViewModel(DataAccess mockDataAccess)
        {
            _dataAccess = mockDataAccess;
            InitializeAll();
        }

        public void InitializeAll()
        {
            _libraryItems = new ObservableCollection<LibraryItem>();
            GetLibraryItemsList();
            _libraryItemsCollection = new CollectionViewSource
            {
                Source = LibraryItems
            };
            _libraryItemsCollection.Filter += (sender, FilterEventArgs) => { CollectionFilter(sender, FilterEventArgs, SearchText); };
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
                _libraryItemsCollection.View.Refresh();
            }
        }

        public ICollectionView LibraryItemCollection
        {
            get
            {
                return _libraryItemsCollection.View;
            }
        }

        public ObservableCollection<LibraryItem> LibraryItems
        {
            get { return _libraryItems; }
            set { SetProperty(ref _libraryItems, value); }
        }

        public ICommand GetLibraryItemsListCommand
        {
            get
            {
                return _getLibraryItemsListCommand ?? (_getLibraryItemsListCommand = new CommandHandler(() => GetLibraryItemsList(), () => true));
            }
        }

        public void GetLibraryItemsList()
        {
            LibraryItems = _dataAccess.GetLibraryItemList();
        }

    }
}
