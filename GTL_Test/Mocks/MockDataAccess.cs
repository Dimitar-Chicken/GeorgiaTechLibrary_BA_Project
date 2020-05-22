using GTL_Application.Interfaces;
using GTL_Application.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GTL_Test.Mocks
{
    [ExcludeFromCodeCoverage]
    public class MockDataAccess : IDataAccess
    {

        public ObservableCollection<ILibraryItemBorrow> GetLibraryItemBorrowsList()
        {
            ObservableCollection<ILibraryItemBorrow> libraryItemBorrows = new ObservableCollection<ILibraryItemBorrow>();

            LibraryItemBorrow libraryItemBorrow1 = new LibraryItemBorrow
            {
                PersonName = "Fredrik Hans",
                Title = "Bad Title",
                ISBN = "9783161484400",
                Status = "On Loan",
                BorrowDate = new DateTime(),
                ReturnDate = new DateTime()
            };
            libraryItemBorrows.Add(libraryItemBorrow1);

            LibraryItemBorrow libraryItemBorrow2 = new LibraryItemBorrow
            {
                PersonName = "Larry Streisand",
                Title = "Good Title",
                ISBN = "9783221484100",
                Status = "On Loan",
                BorrowDate = new DateTime(),
                ReturnDate = new DateTime()
            };
            libraryItemBorrows.Add(libraryItemBorrow2);

            return libraryItemBorrows;
        }

        public ObservableCollection<ILibraryItem> GetLibraryItemList()
        {
            ObservableCollection<ILibraryItem> libraryItems = new ObservableCollection<ILibraryItem>();

            LibraryItem libraryItem1 = new LibraryItem
            {
                Title = "Book of Truths",
                Author = "Tanek Soone",
                SubjectArea = "Sample Subject Area",
                ItemDescription = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                TypeName = "Book"
            };
            libraryItems.Add(libraryItem1);

            LibraryItem libraryItem2 = new LibraryItem
            {
                Title = "Book of Lies",
                Author = "John Doe",
                SubjectArea = "Sample Subject Area",
                ItemDescription = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                TypeName = "Book"
            };
            libraryItems.Add(libraryItem2);

            return libraryItems;
        }
    }
}
