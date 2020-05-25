using GTL_Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Data;

namespace GTL_Application.ViewModel
{
    public class MainWindowViewModel : IMainWindowViewModel, INotifyPropertyChanged
    {
        public string Title { get; set; }
        public MainWindowViewModel()
        {
            Title = "Test";
        }

        #region Property Processing
        protected bool SetProperty<T>(ref T backingStore, T value,
           [CallerMemberName]string propertyName = "",
           Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
            {
                return;
            }

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion Property

        public ObservableCollection<T> FilterList<T>(ObservableCollection<T> collection, ObservableCollection<T> filteredCollection, string SearchText)
        {

            filteredCollection.Clear();
            foreach (T item in collection)
            {
                // Gather a list of all the properties of the LibraryItem object instance.
                PropertyInfo[] props = item.GetType().GetProperties();
                // Iterate over the individual properties and retrieve the values using the Get methods.
                foreach (var p in props)
                {
                    var val = p.GetValue(item);
                    if (val == null)
                        return collection;

                    // If the property contains the SearchText string, set the FilterEventArgs Accepted flag to true in order to display it in the Collection.
                    if (val.ToString().ToUpper().Contains(SearchText.ToUpper()))
                        filteredCollection.Add(item);
                }
            }

            return filteredCollection;
        }
    }
}
