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

        public void CollectionFilter(object sender, FilterEventArgs e, string SearchText)
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                e.Accepted = true;
                return;
            }

            object objectToFilter = e.Item as object;

            // Gather a list of all the properties of the LibraryItem object instance.
            PropertyInfo[] props = objectToFilter.GetType().GetProperties();
            // Iterate over the individual properties and retrieve the values using the Get methods.
            foreach (var p in props)
            {
                var val = p.GetValue(objectToFilter);
                if (val == null)
                    return;

                // If the property contains the SearchText string, set the FilterEventArgs Accepted flag to true in order to display it in the Collection.
                if (val.ToString().ToUpper().Contains(SearchText.ToUpper()))
                {
                    e.Accepted = true;
                    return;
                }
                else
                {
                    e.Accepted = false;
                }
            }
        }
    }
}
