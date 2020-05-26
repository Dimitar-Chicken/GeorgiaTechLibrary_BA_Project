using GalaSoft.MvvmLight.Messaging;
using GTL_Application.Interfaces;
using GTL_Application.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using System.Windows.Input;

namespace GTL_Application.ViewModel
{
    public class PeopleListViewModel : MainWindowViewModel, IPeopleListViewModel
    {
        private string _searchText;
        protected readonly IDataAccess _dataAccess;
        private ObservableCollection<IPerson> _people;
        private ObservableCollection<IPerson> _filtered;
        private ICommand _getFilteredPeopleListCommand;
        private ICommand _openDescriptionWindowCommand;

        public PeopleListViewModel(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            InitializeAll();
        }

        public void InitializeAll()
        {
            _people = new ObservableCollection<IPerson>();
            _filtered = new ObservableCollection<IPerson>();
            GetFilteredPeopleList();
        }
        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;
            }
        }

        public ObservableCollection<IPerson> FilteredPeopleList
        {
            get { return _filtered; }
            set { SetProperty(ref _filtered, value); }
        }

        public ICommand GetFilteredPeopleListCommand
        {
            get
            {
                return _getFilteredPeopleListCommand ?? (_getFilteredPeopleListCommand = new CommandHandler(() => GetFilteredPeopleList(), () => true));
            }
        }

        public ICommand OpenDescriptionWindowCommand
        {
            get
            {
                return _openDescriptionWindowCommand ?? (_openDescriptionWindowCommand = new CommandHandlerWithParameters<object[]>((dates) => OpenDescriptionWindow(dates), () => true));
            }
        }

        public void GetFilteredPeopleList()
        {
            _people = _dataAccess.GetPeople();
            if (string.IsNullOrEmpty(SearchText))
            {
                FilteredPeopleList = _dataAccess.GetPeople();
            }
            else
            {
                FilteredPeopleList = FilterList<IPerson>(_people, _filtered, SearchText);
            }
        }

        public void OpenDescriptionWindow(object[] dates)
        {
            ViewModelCarrier<object[]> viewModelCarrier = new ViewModelCarrier<object[]>(dates);
            Messenger.Default.Send(viewModelCarrier);
        }
    }
}
