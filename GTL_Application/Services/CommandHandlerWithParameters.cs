using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace GTL_Application.Services
{
    class CommandHandlerWithParameters : ICommand
    {
        private Action<string> _action;
        private Func<bool> _canExecute;

        public CommandHandlerWithParameters(Action<string> action, Func<bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            _action(parameter.ToString());
        }
    }
}
