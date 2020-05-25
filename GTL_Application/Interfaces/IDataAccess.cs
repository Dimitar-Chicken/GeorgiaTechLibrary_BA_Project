using GTL_Application.Model;
using System.Collections.ObjectModel;
using System.Security;

namespace GTL_Application.Interfaces
{
    public interface IDataAccess
    {
        public ObservableCollection<ILibraryItem> GetLibraryItemList();
        public ObservableCollection<ILibraryItemBorrow> GetLibraryItemBorrowsList();
        public ObservableCollection<IBorrowableBookCopy> GetBorrowableBookCopiesList();
        public ObservableCollection<IPerson> GetPeople();
        public bool CreateNewBookBorrow(SecureString SSN, IBorrowableBookCopy borrowableBookCopy);
    }
}
