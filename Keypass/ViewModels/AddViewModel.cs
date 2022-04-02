using Keypass.Commands;
using Keypass.Repository.Interfaces;
using Keypass.State.Accounts;
using Keypass.State.Navigators;
using Microsoft.AspNet.Identity;
using MvvmFramework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;
using Keypass.Domain;
using Keypass.Services.Criptography;

namespace Keypass.ViewModels
{
    public class AddViewModel : ViewModelBase
    {

        private string title;

        public string Title
        {
            get { return title; }
            set 
            {
                SetValue(ref title, value);
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

        private DateTime created;

        public DateTime Created
        {
            get { return created; }
            set { SetValue(ref created, value); }
        }

        private string comment;
      
        public string Comment
        {
            get { return comment; }
            set { SetValue(ref comment, value); }
        }

        public bool CanRegister => !(
            string.IsNullOrEmpty(Title) &&
            string.IsNullOrEmpty(Username) &&
            string.IsNullOrEmpty(Password));
            

        public ICommand SaveCommand { get; }
              
        public MessageViewModel ErrorMessageViewModel { get; }

        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }

        public AddViewModel(IUnitOfWork unitOfWork, 
            IRenavigator homeRenavigator,
            IPasswordCryptor passwordCryptor,
            IAccountStore accountStore)
        {
            

            ErrorMessageViewModel = new MessageViewModel();
            Created = DateTime.Now;         

            SaveCommand = new SaveCommand(this,                                          
                                            homeRenavigator,
                                            passwordCryptor,
                                            accountStore,
                                            unitOfWork);
        }

        

        public override void Dispose()
        {
            ErrorMessageViewModel.Dispose();
            base.Dispose();
        }
    }
}
