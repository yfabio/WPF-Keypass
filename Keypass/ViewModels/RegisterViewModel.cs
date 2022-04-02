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
    public class RegisterViewModel : ViewModelBase
    {
        private string email;

        public string Email
        {
            get { return email; }
            set 
            {
                SetValue(ref email, value);               
                OnPropertyChanged(nameof(CanRegister));
            }
        }


        private string username;

        public string Username
        {
            get { return username; }
            set 
            {
                SetValue(ref username, value);                
                OnPropertyChanged(nameof(CanRegister));
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set 
            {
                SetValue(ref password, value);               
                        OnPropertyChanged(nameof(CanRegister));
            }
        }

        private string confirmPassword;

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set 
            {
                SetValue(ref confirmPassword, value);                
                OnPropertyChanged(nameof(CanRegister));
            }
        }

        public bool CanRegister => !(
            string.IsNullOrEmpty(Email) &&
            string.IsNullOrEmpty(Username) &&
            string.IsNullOrEmpty(Password) &&
            string.IsNullOrEmpty(ConfirmPassword));

        public ICommand RegisterCommand { get; }
        public ICommand ViewLoginCommand { get; }

        public MessageViewModel ErrorMessageViewModel { get; }

        public string ErrorMessage 
        {
            set => ErrorMessageViewModel.Message = value;                
        }

        public RegisterViewModel(IAuthenticator authenticator, 
            IRenavigator registerRenavigator,
            IRenavigator loginRenavigator)
        {
            ErrorMessageViewModel = new MessageViewModel();
            RegisterCommand = new RegisterCommand(this, authenticator, loginRenavigator);
            ViewLoginCommand = new RenavigateCommand(loginRenavigator);
        }



        public override void Dispose()
        {
            ErrorMessageViewModel.Dispose();
            base.Dispose();
        }
    }
}
