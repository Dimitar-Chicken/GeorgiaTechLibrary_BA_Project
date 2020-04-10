using System;
using System.Collections.Generic;
using System.Text;

namespace GTL_Application.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        public string Title { get; set; }
        public MainWindowViewModel()
        {
            Title = "Test1";
        }
    }
}
