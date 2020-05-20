using GTL_Application.Interfaces;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GTL_Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainWindow(IMainWindowViewModel mainWindowViewModel, ILibraryItemsListViewModel libraryItemsListViewModel, IBorrowedItemsListViewModel borrowedItemsListViewModel, IPeopleListViewModel peopleListViewModel)
        {
            InitializeComponent();

            this.DataContext = mainWindowViewModel;
            LibraryItemListTab.DataContext = libraryItemsListViewModel;
            BorrowedItemsTab.DataContext = borrowedItemsListViewModel;
            PeopleTab.DataContext = peopleListViewModel;
        }

        private void EnableListViewScrolling(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrowViewer = (ScrollViewer)sender;
            scrowViewer.ScrollToVerticalOffset(scrowViewer.VerticalOffset - e.Delta/6);
            e.Handled = true;
        }
    }
}
