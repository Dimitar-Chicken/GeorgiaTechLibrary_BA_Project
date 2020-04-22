using GTL_Application.Model;
using GTL_Application.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;

namespace GTL_Application.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public string Title { get; set; }
        public MainWindowViewModel()
        {
            Title = "Test";
        }
    }
}
