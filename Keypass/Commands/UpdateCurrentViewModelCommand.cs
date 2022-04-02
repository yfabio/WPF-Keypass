using Keypass.State.Navigators;
using Keypass.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Keypass.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly INavigator _navigator;
        private readonly IKeyPassViewModelFactory _viewModelFactory;


        public UpdateCurrentViewModelCommand(INavigator navigator, IKeyPassViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewType)
            {
                ViewType viewType = (ViewType) parameter;
                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
                CanExecuteChanged?.Invoke(this, new EventArgs());
            }
        }
    }
}
