using GTL_Application.Interfaces;
using GTL_Application.Services;
using GTL_Application.View;
using GTL_Application.ViewModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace GTL_Application
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    [ExcludeFromCodeCoverage]
    public partial class App : Application
    {
        IDataAccess dataAccess;
        IMainWindowViewModel mainWindowViewModel;
        ILibraryItemsListViewModel libraryItemsListViewModel;
        IBorrowedItemsListViewModel borrowedItemsListViewModel;
        IPeopleListViewModel peopleListViewModel;

        public App()
        {
            
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            dataAccess = new DataAccess();

            // Setting the DataContext to the tabs to be the appropriate ViewModels.
            mainWindowViewModel = new MainWindowViewModel();
            libraryItemsListViewModel = new LibraryItemsListViewModel(dataAccess);
            borrowedItemsListViewModel = new BorrowedItemsListViewModel(dataAccess);
            peopleListViewModel = new PeopleListViewModel();

            // Application is running
            // Process command line args
            bool startMinimized = false;
            for (int i = 0; i != e.Args.Length; ++i)
            {
                if (e.Args[i] == "/StartMinimized")
                {
                    startMinimized = true;
                }
            }

            // Create main application window, starting minimized if specified
            MainWindow mainWindow = new MainWindow(mainWindowViewModel, libraryItemsListViewModel, borrowedItemsListViewModel, peopleListViewModel);
            if (startMinimized)
            {
                mainWindow.WindowState = WindowState.Minimized;
            }
            mainWindow.Show();
        }
    }
}
