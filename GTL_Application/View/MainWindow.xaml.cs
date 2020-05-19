using GTL_Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GTL_Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        MainWindowViewModel mainWindowViewModel;
        LibraryItemsListViewModel libraryItemsListViewModel;
        BorrowedItemsListViewModel borrowedItemsListViewModel;
        PeopleListViewModel peopleListViewModel;
        public MainWindow()
        {
            InitializeComponent();

            // Setting the DataContext to the tabs to be the appropriate ViewModels.
            mainWindowViewModel = new MainWindowViewModel();
            libraryItemsListViewModel = new LibraryItemsListViewModel();
            borrowedItemsListViewModel = new BorrowedItemsListViewModel();
            peopleListViewModel = new PeopleListViewModel();

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
