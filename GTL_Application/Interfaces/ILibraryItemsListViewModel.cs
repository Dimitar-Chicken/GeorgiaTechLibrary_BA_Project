using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GTL_Application.Interfaces
{
    public interface ILibraryItemsListViewModel
    {
        public void InitializeAll();

        public void GetFilteredLibraryItemsList();

    }
}
