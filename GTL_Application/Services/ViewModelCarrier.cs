using System;
using System.Collections.Generic;
using System.Text;

namespace GTL_Application.Services
{
    public class ViewModelCarrier<T>
    {
        public T viewModel;

        public ViewModelCarrier(T givenViewModel)
        {
            viewModel = givenViewModel;
        }
    }
}
