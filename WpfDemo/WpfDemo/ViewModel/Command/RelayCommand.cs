using System;
using System.Windows.Input;

namespace WpfDemo.ViewModel.Command
{
    public class RelayCommand : ICommand
    {
        private Action<object> _executeMethod;
        private Func<object, bool> _canexecuteMethod;

        public RelayCommand(Action<object> executeMethod, Func<object, bool> canexecuteMethod)
        {
            _executeMethod = executeMethod;
            _canexecuteMethod = canexecuteMethod;
        }

        public event EventHandler CanExecuteChanged// ha a property changes akkor canexecute triggerelodik
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) //=> canexecuteMethod(parameter); // ami eldonti h can or cannot be executed
        {
            return _canexecuteMethod(parameter);
        }

        public void Execute(object parameter) => _executeMethod(parameter); //Action tortenik itt

    }
}
