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
    /// Interaction logic for LibraryItemDescriptionWindow.xaml
    /// </summary>
    public partial class LibraryItemDescriptionWindow : Window
    {
        public LibraryItemDescriptionWindow(string itemDescription)
        {
            InitializeComponent();
            textBlock.Text = itemDescription;
        }
    }
}
