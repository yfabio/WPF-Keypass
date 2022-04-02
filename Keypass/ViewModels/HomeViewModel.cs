using Keypass.Repository;
using Keypass.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Keypass.State.Accounts;
using Keypass.Repository.Interfaces;
using System.Reflection;
using System.Windows.Input;
using MvvmFramework;
using System.Diagnostics;
using Keypass.Services.Criptography;
using Keypass.Commands;
using Keypass.State.Navigators;
using MvvmFramework.Util;
using Keypass.Exceptions;
using Keypass.ViewModels.WindowServices;

namespace Keypass.ViewModels
{
    public class HomeViewModel : ViewModelBase, IHomeViewService 
    {
        private readonly IPasswordCryptor _passwordCryptor;       
        private readonly IUnitOfWork _uniOfWork;
               
        private IEnumerable<Service> services;

        public IEnumerable<Service> Services
        {
            get => services;
            set { SetValue(ref services, value); }
        }

        public Service Service { get; set; }

        private CommandParameter deleteCommand;
        private CommandParameter editCommand;
        private CommandParameter editModeCommand;
        private Command cancelCommand;
        private Command searchCommand;
        private Command clearTextCommand;


        public CommandParameter EditModeCommand
        {
            get => editModeCommand;
        }
        public Command CancelCommand
        {
            get => cancelCommand;
        }

        public CommandParameter DeleteCommand
        {
            get => deleteCommand;
        }

        public CommandParameter EditCommand
        {
            get => editCommand;
        }


        public Command SearchCommand
        {
            get => searchCommand;
        }


        public Command ClearTextCommand
        {
            get => clearTextCommand;
        }

        public HomeViewModel(
            IPasswordCryptor passwordCryptor,
            IAccountStore accountStore,
            IUnitOfWork uniOfWork)
        {
            _passwordCryptor = passwordCryptor;            
            _uniOfWork = uniOfWork;

            ErrorMessageViewModel = new MessageViewModel();

            Services = _uniOfWork.Services.SortByDate();

            PassMode = !EditMode;

            editModeCommand = new CommandParameter(OnEditModeHandler);
            cancelCommand = new Command(OnCancelHandler);
            deleteCommand = new CommandParameter(OnDeleteHandler);
            editCommand = new CommandParameter(OnEditHandler);
            searchCommand = new Command(OnSearchHandler);
            clearTextCommand = new Command(OnClearTextHandler);
        }

        private void OnClearTextHandler()
        {
            SearchText = string.Empty;
        }

        private void OnSearchHandler()
        {
            if (!string.IsNullOrEmpty(SearchText))
            {
                Services = _uniOfWork.Services.SearchByText(SearchText);
                CleanUp();
            }
        }

        private void OnEditHandler(object obj)
        {

            if (Routine.IsValueEmpty(Title))
            {
                ErrorMessage = "Title is required!";
            }
            else
            {
                if (Routine.IsValueEmpty(Username))
                {
                    ErrorMessage = "Username is required!";
                }
                else
                {
                    if (Routine.IsValueEmpty(Password))
                    {
                        ErrorMessage = "Password is required!";
                    }
                    else if (Routine.IsValueEmpty(Created.ToString()))
                    {
                        ErrorMessage = "Date is required!";

                    }
                    else
                    {
                        if (obj is Service service)
                        {
                            Service dbService = _uniOfWork.Services.Get(service.Id);
                            dbService.Title = Title;
                            dbService.Username = Username;
                            dbService.Password = _passwordCryptor.Encrypt(Password);
                            dbService.Created = Created;
                            dbService.Comment = Comment;
                            _uniOfWork.Save();
                            SelectedRadioBox(RadioContent);
                            OnCancelHandler();
                            CleanUp();
                        }
                    }
                }
            }

        }

        private void OnDeleteHandler(object obj)
        {
            if (obj is Service service)
            {
                _uniOfWork.Services.Remove(service);
                _uniOfWork.Save();
                Services = _uniOfWork.Services.GetAll();
                OnCancelHandler();
                CleanUp();
            }
        }

        private void CleanUp()
        {
            Title = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
            Created = DateTime.Now;
            Comment = string.Empty;
            ErrorMessage = string.Empty;
        }

        private void OnCancelHandler()
        {
            EditMode = false;
            PassMode = !EditMode;           
            CleanUp();
        }

        private void OnEditModeHandler(object obj)
        {
            EditMode = true;
            PassMode = !EditMode;

            

            if (obj is Service service)
            {
                Service = _uniOfWork.Services.Get(service.Id);
                Title = Service.Title;
                Username = Service.Username;

                try
                {
                    Password = _passwordCryptor.Decrypt(Service.Password);
                }
                catch (NoSuchEncryptedPasswordExecption)
                {
                    Password = Service.Password;                   
                }

                Created = Service.Created;
                Comment = Service.Comment;
            }

        }

        private string title;

        public string Title
        {
            get { return title; }
            set { SetValue(ref title, value); }
        }

        private string username;

        public string Username
        {
            get { return username; }
            set { SetValue(ref username, value); }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { SetValue(ref password, value); }
        }

        private DateTime created;

        public DateTime Created
        {
            get
            {
                return created;
            }
            set { SetValue(ref created, value); }
        }

        private string comment;

        public string Comment
        {
            get { return comment; }
            set { SetValue(ref comment, value); }
        }


        public bool editMode;

        public bool EditMode
        {
            get { return editMode; }
            set { SetValue(ref editMode, value); }
        }

        private bool passMode;

        public bool PassMode
        {
            get { return passMode; }
            set { SetValue(ref passMode, value); }
        }

        private string RadioContent { get; set; }

        public void SelectionChanged(Service service)
        {
            if (service != null)
            {
                Title = service.Title;
                Username = service.Username;
                Password = service.Password;
                Created = service.Created;
                Comment = service.Comment;
            }
        }

       

        private string searchText;

        public string SearchText
        {
            get { return searchText; }
            set { SetValue(ref searchText, value); }
        }

        public MessageViewModel ErrorMessageViewModel { get; }

        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }
             

        public void Checked()
        {
            if (Password != null && Services.Count() >= 1)
            {
                string decrypted = _passwordCryptor.Decrypt(Password);
                Password = decrypted;
            }
        }

        public void Unchecked()
        {
            if (Password != null && Services.Count() >= 1)
            {
                string encrypted = _passwordCryptor.Encrypt(Password);
                Password = encrypted;
            }
        }

        public void SearchBoxEmptyEvent()
        {
            Services = _uniOfWork.Services.GetAll();
        }

        public void SelectedRadioBox(string content)
        {
            if ("Date".Equals(content))
            {

                Services = _uniOfWork.Services.SortByDate();
            }
            else if ("Title".Equals(content))
            {
                Services = _uniOfWork.Services.SortByTitle();
            }
            else
            {
                Services = _uniOfWork.Services.SortByUsername();
            }

            RadioContent = content;
        }

        public void OnInputSearchHandler()
        {
            OnSearchHandler();
        }

        
    }
}
