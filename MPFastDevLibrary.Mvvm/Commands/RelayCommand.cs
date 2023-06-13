using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace MPFastDevLibrary.Mvvm.Commands
{
    public class RelayCommand : CommandBase
    {

        private readonly Action<object> _executeMethod;
        private readonly Func<bool> _canExecuteMethod;



        public RelayCommand(Action<object> executeMethod) 
            : this(executeMethod, () => true)
        {

        }

        public RelayCommand(Action<object> executeMethod, Func<bool> canExecuteMethod)
        {
            if (executeMethod == null || canExecuteMethod == null)
                throw new ArgumentNullException(nameof(executeMethod), "RelayCommand Delegates Can not be Null");

            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }


        protected override bool CanExecute(object parameter)
        {
            return _canExecuteMethod();
        }

        protected override void Execute(object parameter)
        {
            _executeMethod(parameter);
        }
    }
}
