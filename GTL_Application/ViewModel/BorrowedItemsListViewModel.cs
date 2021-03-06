﻿using GalaSoft.MvvmLight.Messaging;
using GTL_Application.Interfaces;
using GTL_Application.Model;
using GTL_Application.Services;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;

namespace GTL_Application.ViewModel
{
    public class BorrowedItemsListViewModel : MainWindowViewModel, IBorrowedItemsListViewModel
    {
        private string _searchText;
        private readonly IDataAccess _dataAccess;
        private ObservableCollection<ILibraryItemBorrow> _libraryItemBorrows;
        private ObservableCollection<ILibraryItemBorrow> _filtered;
        private ICommand _getFilteredLibraryItemBorrowsListCommand;
        private ICommand _openNewEntryWindowCommand;

        public BorrowedItemsListViewModel(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            InitializeAll();
        }

        public void InitializeAll()
        {
            _libraryItemBorrows = new ObservableCollection<ILibraryItemBorrow>();
            _filtered = new ObservableCollection<ILibraryItemBorrow>();
            GetFilteredLibraryItemBorrowsList();
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

        public ObservableCollection<ILibraryItemBorrow> FilteredLibraryItemBorrows
        {
            get { return _filtered; }
            set { SetProperty(ref _filtered, value); }
        }

        public ICommand GetFilteredLibraryItemBorrowsListCommand
        {
            get
            {
                return _getFilteredLibraryItemBorrowsListCommand ?? (_getFilteredLibraryItemBorrowsListCommand = new CommandHandler(() => GetFilteredLibraryItemBorrowsList(), () => true));
            }
        }

        public ICommand OpenNewEntryWindowCommand
        {
            get
            {
                return _openNewEntryWindowCommand ?? (_openNewEntryWindowCommand = new CommandHandler(() => OpenNewEntryWindow(), () => true));
            }
        }

        public void GetFilteredLibraryItemBorrowsList()
        {
            _libraryItemBorrows = _dataAccess.GetLibraryItemBorrowsList();
            if (string.IsNullOrEmpty(SearchText))
            {
                FilteredLibraryItemBorrows = _dataAccess.GetLibraryItemBorrowsList();
            }
            else
            {
                FilteredLibraryItemBorrows = FilterList<ILibraryItemBorrow>(_libraryItemBorrows, _filtered, SearchText);
            }
        }

        public void OpenNewEntryWindow()
        {
            NewBorrowedItemEntryViewModel newBorrowedItemEntryViewModel = new NewBorrowedItemEntryViewModel(_dataAccess);
            ViewModelCarrier<INewBorrowedItemEntryViewModel> viewModelCarrier = new ViewModelCarrier<INewBorrowedItemEntryViewModel>(newBorrowedItemEntryViewModel);
            Messenger.Default.Send(viewModelCarrier);
        }
    }
}