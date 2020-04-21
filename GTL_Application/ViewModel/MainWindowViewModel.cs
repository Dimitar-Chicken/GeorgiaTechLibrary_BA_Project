using GTL_Application.Model;
using GTL_Application.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;

namespace GTL_Application.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public string Title { get; set; }
        private string _searchText;
        protected readonly LibraryItemDataAccess _libraryItemDataAccess;
        private ObservableCollection<LibraryItem> _libraryItems;
        private CollectionViewSource _libraryItemsCollection;
        private ICommand _getLibraryItemsListCommand;
        public MainWindowViewModel()
        {
            _libraryItemDataAccess = new LibraryItemDataAccess();
            InitializeAll();
        }
        public MainWindowViewModel(LibraryItemDataAccess mockLibraryItemDataAccess)
        {
            _libraryItemDataAccess = mockLibraryItemDataAccess;
            InitializeAll();
        }

        public void InitializeAll()
        {
            Title = "Test1";
            _libraryItems = new ObservableCollection<LibraryItem>();
            GetLibraryItemsList();
            _libraryItemsCollection = new CollectionViewSource
            {
                Source = LibraryItems
            };
            _libraryItemsCollection.Filter += LibraryItemsCollection_Filter;
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

        public ICollectionView SourceCollection
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


        private void LibraryItemsCollection_Filter(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                e.Accepted = true;
                return;
            }

            LibraryItem libraryItem = e.Item as LibraryItem;
            
            // Gather a list of all the properties of the LibraryItem object instance.
            PropertyInfo[] props = libraryItem.GetType().GetProperties();
            // Iterate over the individual properties and retrieve the values using the Get methods.
            foreach (var p in props)
            {
                var val = p.GetValue(libraryItem);
                if (val == null)
                    return;

                // If the property contains the SearchText string, set the FilterEventArgs Accepted flag to true in order to display it in the Collection.
                if (val.ToString().ToUpper().Contains(SearchText.ToUpper()))
                {
                    e.Accepted = true;
                    return;
                }
                else
                {
                    e.Accepted = false;
                }
            }
        }

        public void GetLibraryItemsList()
        {
            LibraryItems = _libraryItemDataAccess.GetLibraryItemList();
        }


    }
}
