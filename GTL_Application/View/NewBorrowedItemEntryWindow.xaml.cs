using GTL_Application.Interfaces;
using GTL_Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GTL_Application.View
{
    /// <summary>
    /// Interaction logic for NewBorrowedItemEntryWindow.xaml
    /// </summary>
    public partial class NewBorrowedItemEntryWindow : Window
    {
        public NewBorrowedItemEntryWindow(INewBorrowedItemEntryViewModel newBorrowedItemEntryViewModel)
        {
            InitializeComponent();
            DataContext = newBorrowedItemEntryViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
