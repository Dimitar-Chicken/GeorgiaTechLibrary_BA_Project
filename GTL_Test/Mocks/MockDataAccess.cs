using GTL_Application.Interfaces;
using GTL_Application.Model;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Security;

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
                Status = "Overdue",
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
                Authors = "Tanek Soone",
                SubjectArea = "Sample Subject Area",
                ItemDescription = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                TypeName = "Book"
            };
            libraryItems.Add(libraryItem1);

            LibraryItem libraryItem2 = new LibraryItem
            {
                Title = "Book of Lies",
                Authors = "John Doe",
                SubjectArea = "Sample Subject Area",
                ItemDescription = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                TypeName = "Book"
            };
            libraryItems.Add(libraryItem2);

            return libraryItems;
        }

        public ObservableCollection<IBorrowableBookCopy> GetBorrowableBookCopiesList()
        {
            ObservableCollection<IBorrowableBookCopy> borrowableBookCopies = new ObservableCollection<IBorrowableBookCopy>();

            BorrowableBookCopy borrowableBookCopy1 = new BorrowableBookCopy
            {
                ID = ConvertToSecureString("42"),
                Authors = "Jane Brow, Patrick Stewart",
                Title = "Book of Pineapples"
            };
            borrowableBookCopies.Add(borrowableBookCopy1);

            BorrowableBookCopy borrowableBookCopy2 = new BorrowableBookCopy
            {
                ID = ConvertToSecureString("17"),
                Authors = "Ben Sonsen, Peter Larson",
                Title = "Book of Cucumbers"
            };
            borrowableBookCopies.Add(borrowableBookCopy2);

            return borrowableBookCopies;
        }

        public ObservableCollection<IPerson> GetPeople()
        {
            ObservableCollection<IPerson> people = new ObservableCollection<IPerson>();

            Person person1 = new Person
            {
                SSN = new NetworkCredential("", "168805189225").SecurePassword,
                PersonName = "John Doe",
                Type = "Member",
                Address = "Test Address #1",
                Phone = "1234567890",
                Email = "email1@example.com"
            };
            people.Add(person1);

            Person person2 = new Person
            {
                SSN = new NetworkCredential("", "161104142128").SecurePassword,
                PersonName = "Jane Boe",
                Type = "Professor",
                Address = "Test Address #2",
                Phone = "0987654321",
                Email = "email2@example.com"
            };
            people.Add(person2);

            return people;
        }

        public bool CreateNewBookBorrow(SecureString SSN, IBorrowableBookCopy borrowableBookCopy)
        {
            if (borrowableBookCopy == null || SSN == null)
            {
                return false;
            }
            return true;
        }

        public SecureString ConvertToSecureString(string input)
        {
            if (input == null)
                return null;

            SecureString result = new SecureString();
            foreach (char c in input)
            {
                result.AppendChar(c);
            }

            return result;
        }
    }
}
