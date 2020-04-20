using GTL_Application.Model;
using GTL_Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace GTL_Application.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public string Title { get; set; }
        protected readonly LibraryItemDataAccess _libraryItemDataAccess;
        List<LibraryItem> libraryItems = new List<LibraryItem>();
        public List<LibraryItem> LibraryItems
        {
            get { return libraryItems; }
            set { SetProperty(ref libraryItems, value); }
        }

        private ICommand _getLibraryItemsListCommand;
        public ICommand GetLibraryItemsListCommand
        {
            get
            {
                return _getLibraryItemsListCommand ?? (_getLibraryItemsListCommand = new CommandHandler(() => GetLibraryItemsList(), () => true));
            }
        }
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
        }

        public void GetLibraryItemsList()
        {
            LibraryItems = _libraryItemDataAccess.GetLibraryItemList();
        }


    }
}
