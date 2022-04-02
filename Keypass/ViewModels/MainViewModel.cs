using Keypass.Commands;
using Keypass.State.Accounts;
using Keypass.State.Authenticators;
using Keypass.State.Navigators;
using Keypass.ViewModels.Factories;
using Keypass.ViewModels.WindowServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Keypass.ViewModels
{
    public class MainViewModel : ViewModelBase, IMainWindowService
    {
        private readonly IKeyPassViewModelFactory _viewModelFactory;
        private readonly INavigator _navigator;
        private readonly IAuthenticator _authenticator;
        private readonly IAccountStore _accountStore;
       
        public bool IsLoggedIn => _authenticator.IsLoggedIn;
        public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;

        private string loggedUser;

        public string LoggedUser
        {
            get { return loggedUser; }
            set { SetValue(ref loggedUser, value); }
        }

        private string version;

        public string Version
        {
            get { return version; }
            set { SetValue(ref version, value); }
        }

        public ICommand UpdateCurrentViewModelCommand { get; }
               
        public MainViewModel(IKeyPassViewModelFactory viewModelFactory, 
                            INavigator navigator, 
                            IAuthenticator authenticator,
                            IAccountStore accountStore)
        {
            _viewModelFactory = viewModelFactory;
            _navigator = navigator;
            _authenticator = authenticator;
            _accountStore = accountStore;


            _navigator.StateChanged += Navigator_StateChanged;
            _authenticator.StateChanged += Authenticator_StateChanged;
            

            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(_navigator,_viewModelFactory);
            UpdateCurrentViewModelCommand.Execute(ViewType.Login);

            CurrentViewModel.LoginSucceed += OnLoginSucceed;
        }

        private void OnLoginSucceed()
        {
            SetStatusBar();
        }

        private void Authenticator_StateChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
        }

        private void Navigator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));            
        }

        public override void Dispose()
        {
            _navigator.StateChanged -= Navigator_StateChanged;
            base.Dispose();
        }


        public void SetStatusBar()
        {
            Version = string.Format("Verison: {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
            LoggedUser = string.Format("Logged User: {0}", _accountStore.CurrentAccount.Username);
        }


        public void OnClose() 
        {
            CurrentViewModel.LoginSucceed -= OnLoginSucceed;            
        }


    }
}
