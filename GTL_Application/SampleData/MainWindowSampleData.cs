using GTL_Application.Interfaces;
using GTL_Application.Model;
using GTL_Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Data;

namespace GTL_Application.SampleData
{
    class MainWindowSampleData : MainWindowViewModel
    {
        private ObservableCollection<ILibraryItem> _libraryItems;
        private ObservableCollection<ILibraryItemBorrow> _libraryItemBorrows;

        public MainWindowSampleData()
        {
            Title = "Sample";
            _libraryItems = new ObservableCollection<ILibraryItem>();
            LibraryItem libraryItem1 = new LibraryItem
            {
                Title = "Sample Title",
                Author = "Sample Author",
                SubjectArea = "Sample Subject Area",
                ItemDescription = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                TypeName = "Book"
            };
            _libraryItems.Add(libraryItem1);

            _libraryItemBorrows = new ObservableCollection<ILibraryItemBorrow>();
            LibraryItemBorrow libraryItemBorrow1 = new LibraryItemBorrow
            {
                PersonName = "John Doe",
                Title = "Test Title",
                ISBN = "9783161484100",
                Status = "On Loan",
                BorrowDate = new DateTime(),
                ReturnDate = new DateTime()
            };

            _libraryItemBorrows.Add(libraryItemBorrow1);
        }

        public ObservableCollection<ILibraryItem> FilteredLibraryItems
        {
            get { return _libraryItems; }
            set { SetProperty(ref _libraryItems, value); }
        }

        public ObservableCollection<ILibraryItemBorrow> FilteredLibraryItemBorrows
        {
            get { return _libraryItemBorrows; }
            set { SetProperty(ref _libraryItemBorrows, value); }
        }
    }
}
