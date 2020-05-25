using GalaSoft.MvvmLight.Messaging;
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
        private ICommand _getFilteredLibraryItemsListCommand;
        private ICommand _openDescriptionWindowCommand;

        public LibraryItemsListViewModel(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            InitializeAll();
        }

        public void InitializeAll()
        {
            _libraryItems = new ObservableCollection<ILibraryItem>();
            _filtered = new ObservableCollection<ILibraryItem>();
            GetFilteredLibraryItemsList();
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

        public ICommand GetFilteredLibraryItemsListCommand
        {
            get
            {
                return _getFilteredLibraryItemsListCommand ?? (_getFilteredLibraryItemsListCommand = new CommandHandler(() => GetFilteredLibraryItemsList(), () => true));
            }
        }

        public ICommand OpenDescriptionWindowCommand
        {
            get
            {
                return _openDescriptionWindowCommand ?? (_openDescriptionWindowCommand = new CommandHandlerWithParameters<string>((itemDescription) => OpenItemDescriptionWindow(itemDescription), () => true));
            }
        }

        public void GetFilteredLibraryItemsList()
        {
            _libraryItems = _dataAccess.GetLibraryItemList();
            if (string.IsNullOrEmpty(SearchText))
            {
                FilteredLibraryItems = _dataAccess.GetLibraryItemList();
            }
            else
            {
                FilteredLibraryItems = FilterList<ILibraryItem>(_libraryItems, _filtered, SearchText);
            }
        }

        public void OpenItemDescriptionWindow(string itemDescription)
        {
            ViewModelCarrier<string> viewModelCarrier = new ViewModelCarrier<string>(itemDescription);
            Messenger.Default.Send(viewModelCarrier);
        }
    }
}
