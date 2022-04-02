using Keypass.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Keypass.ViewModels.ViewModelBase;

namespace Keypass.ViewModels.Factories
{
    public class KeyPassViewModelFactory : IKeyPassViewModelFactory
    {
        private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;
        private readonly CreateViewModel<AddViewModel> _createAddViewModel;
        private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;
        private readonly CreateViewModel<OptionViewModel> _createOptionViewModel;

        public KeyPassViewModelFactory(CreateViewModel<HomeViewModel> createHomeViewModel, CreateViewModel<AddViewModel> createAddViewModel, CreateViewModel<LoginViewModel> createLoginViewModel, CreateViewModel<OptionViewModel> createOptionViewModel)
        {
            _createHomeViewModel = createHomeViewModel;
            _createAddViewModel = createAddViewModel;
            _createLoginViewModel = createLoginViewModel;
            _createOptionViewModel = createOptionViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Login:
                    return _createLoginViewModel();
                case ViewType.Home:
                    return _createHomeViewModel();
                case ViewType.Add:
                    return _createAddViewModel();
                case ViewType.Option:
                    return _createOptionViewModel();
                default:
                    throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType");
            }
        }
    }
}
