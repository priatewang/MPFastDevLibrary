using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MPFastDevLibrary.Mvvm.Commands
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private bool _isEnableExecute = true;

        public bool IsEnableExecute
        {
            get { return _isEnableExecute; }
            set
            {
                if (_isEnableExecute != value)
                {
                    _isEnableExecute = value;
                    if (CanExecuteChanged != null)
                    {
                        CanExecuteChanged(this, new EventArgs());
                    }
                }
            }
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute(parameter);
        }

        void ICommand.Execute(object parameter)
        {
            Execute(parameter);
        }

        protected abstract bool CanExecute(object parameter);

        protected abstract void Execute(object parameter);
    }
}
