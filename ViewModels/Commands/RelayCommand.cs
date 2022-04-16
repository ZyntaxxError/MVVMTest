using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMTest.ViewModels.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> ExecuteMethod;
        private readonly Func<object, bool> CanExecuteMethod;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            ExecuteMethod = execute;
            CanExecuteMethod = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteMethod == null || CanExecuteMethod(parameter);
        }

        public void Execute(object parameter)
        {
            ExecuteMethod(parameter);
        }
    }
}
