using GTL_Application.Model;
using GTL_Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Data;

namespace GTL_Application.SampleData
{
    class MainWindowSampleData : BaseViewModel
    {
        public string Title { get; set; }
        private ObservableCollection<LibraryItem> _libraryItems;
        private ObservableCollection<LibraryItemBorrow> _libraryItemBorrows;
        private CollectionViewSource _libraryItemsCollection;
        private CollectionViewSource _libraryItemBorrowsCollection;

        public MainWindowSampleData()
        {
            Title = "Sample";
            _libraryItems = new ObservableCollection<LibraryItem>();
            LibraryItem libraryItem1 = new LibraryItem
            {
                Title = "Sample Title",
                Author = "Sample Author",
                SubjectArea = "Sample Subject Area",
                ItemDescription = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                TypeName = "Book"
            };
            _libraryItems.Add(libraryItem1);
            _libraryItemsCollection = new CollectionViewSource
            {
                Source = _libraryItems
            };

            _libraryItemBorrows = new ObservableCollection<LibraryItemBorrow>();
            LibraryItemBorrow libraryItemBorrow1 = new LibraryItemBorrow
            {
                PersonName = "John Doe",
                Title = "Test Title",
                ISBN = "9783161484100",
                BorrowDate = new DateTime(),
                ReturnDate = new DateTime()
            };

            _libraryItemBorrows.Add(libraryItemBorrow1);
            _libraryItemBorrowsCollection = new CollectionViewSource
            {
                Source = _libraryItemBorrows
            };
        }
        public ICollectionView LibraryItemCollection
        {
            get
            {
                return _libraryItemsCollection.View;
            }
        }

        public ICollectionView LibraryItemBorrowsCollection
        {
            get
            {
                return _libraryItemBorrowsCollection.View;
            }
        }
    }
}
