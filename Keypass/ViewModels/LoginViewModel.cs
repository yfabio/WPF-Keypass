using Keypass.Commands;
using Keypass.State.Authenticators;
using Keypass.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Keypass.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {

        private string username;

        public string Username
        {
            get { return username; }
            set 
            {
                SetValue(ref username, value);
                OnPropertyChanged(nameof(CanLogin));
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set 
            {
                SetValue(ref password, value);
                OnPropertyChanged(nameof(CanLogin));
            }
        }

        public bool CanLogin => !(string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password));

        public MessageViewModel ErrorMessageViewModel { get; }

        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }

        public ICommand LoginCommand { get; }
        public ICommand ViewRegisterCommand { get; }


        public LoginViewModel(IAuthenticator authenticator,
            IRenavigator loginRenavigator,
            IRenavigator registerRenavigator)
        {
            ErrorMessageViewModel = new MessageViewModel();

            LoginCommand = new LoginCommand(this, authenticator, loginRenavigator);
            ViewRegisterCommand = new RenavigateCommand(registerRenavigator);
            
        }



        public override void Dispose()
        {
            ErrorMessageViewModel.Dispose();
            base.Dispose();
        }
    }
}
