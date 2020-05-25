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
    /// Interaction logic for PersonMembershipDatesWindow.xaml
    /// </summary>
    public partial class PersonMembershipDatesWindow : Window
    {
        public PersonMembershipDatesWindow(object[] dates)
        {
            InitializeComponent();
            
            startDateTextBlock.Text = ((DateTime) dates[0]).ToString();
            endDateTextBlock.Text = ((DateTime) dates[1]).ToString();
        }
    }
}
