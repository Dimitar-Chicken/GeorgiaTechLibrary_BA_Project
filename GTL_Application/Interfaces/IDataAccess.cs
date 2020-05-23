using GTL_Application.Model;
using System.Collections.ObjectModel;

namespace GTL_Application.Interfaces
{
    public interface IDataAccess
    {
        public ObservableCollection<ILibraryItem> GetLibraryItemList();
        public ObservableCollection<ILibraryItemBorrow> GetLibraryItemBorrowsList();
        public ObservableCollection<IBorrowableBookCopy> GetBorrowableBookCopiesList();
    }
}
