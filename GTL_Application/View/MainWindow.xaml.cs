using GalaSoft.MvvmLight.Messaging;
using GTL_Application.Interfaces;
using GTL_Application.Services;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GTL_Application.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [ExcludeFromCodeCoverage]
    public partial class MainWindow : Window
    {
        public MainWindow(IMainWindowViewModel mainWindowViewModel, ILibraryItemsListViewModel libraryItemsListViewModel, IBorrowedItemsListViewModel borrowedItemsListViewModel, IPeopleListViewModel peopleListViewModel)
        {
            InitializeComponent();

            this.DataContext = mainWindowViewModel;
            LibraryItemListTab.DataContext = libraryItemsListViewModel;
            BorrowedItemsTab.DataContext = borrowedItemsListViewModel;
            PeopleTab.DataContext = peopleListViewModel;

            Messenger.Default.Register<ViewModelCarrier<INewBorrowedItemEntryViewModel>>(this, (action) =>
            {
                NewBorrowedItemEntryWindow newBorrowedItemEntryWindow = new NewBorrowedItemEntryWindow(action.viewModel);
                newBorrowedItemEntryWindow.Show();
            });

            Messenger.Default.Register<ViewModelCarrier<string>>(this, (action) =>
            {
                LibraryItemDescriptionWindow libraryItemDescriptionWindow = new LibraryItemDescriptionWindow(action.viewModel);
                libraryItemDescriptionWindow.Show();
            });

            Messenger.Default.Register<ViewModelCarrier<object[]>>(this, (action) =>
            {
                PersonMembershipDatesWindow personMembershipDatesWindow = new PersonMembershipDatesWindow(action.viewModel);
                personMembershipDatesWindow.Show();
            });
        }

        private void EnableListViewScrolling(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrowViewer = (ScrollViewer)sender;
            scrowViewer.ScrollToVerticalOffset(scrowViewer.VerticalOffset - e.Delta/6);
            e.Handled = true;
        }
    }
}
