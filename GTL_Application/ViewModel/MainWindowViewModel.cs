using GTL_Application.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace GTL_Application.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public string Title { get; set; }

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
        public LibraryItemDataAccess libraryItemDataAccess;
        public MainWindowViewModel()
        {
            Title = "Test1";
        }

        public void GetLibraryItemsList()
        {
            libraryItemDataAccess = new LibraryItemDataAccess();
            LibraryItems = libraryItemDataAccess.GetLibraryItemList();
        }


    }
}
