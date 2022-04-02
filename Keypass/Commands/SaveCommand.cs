using Keypass.Domain;
using Keypass.Repository.Interfaces;
using Keypass.Services.Criptography;
using Keypass.State.Accounts;
using Keypass.State.Navigators;
using Keypass.ViewModels;
using MvvmFramework.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keypass.Commands
{
    public class SaveCommand : AsyncCommandBase
    {
        private readonly AddViewModel _addViewModel;
        private readonly IRenavigator _homeRenavigator;
        private readonly IPasswordCryptor _passwordCryptor;
        private readonly IAccountStore _accountStore;
        private readonly IUnitOfWork _unitOfWork;


        public SaveCommand(AddViewModel addViewModel,
            IRenavigator homeRenavigator,
            IPasswordCryptor passwordCryptor,
            IAccountStore accountStore,
            IUnitOfWork unitOfWork)
        {
            _addViewModel = addViewModel;
            _homeRenavigator = homeRenavigator;
            _passwordCryptor = passwordCryptor;
            _accountStore = accountStore;
            _unitOfWork = unitOfWork;

            _addViewModel.PropertyChanged += AddViewModel_PropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return _addViewModel.CanRegister && base.CanExecute(parameter);
        }

        public override void ExecuteAsync(object parameter)
        {

            if (Routine.IsValueEmpty(_addViewModel.Title))
            {
                _addViewModel.ErrorMessage = "Title is required!";
            }
            else
            {
                if (Routine.IsValueEmpty(_addViewModel.Username))
                {
                    _addViewModel.ErrorMessage = "Username is required!";
                }
                else
                {
                    if (Routine.IsValueEmpty(_addViewModel.Password))
                    {
                        _addViewModel.ErrorMessage = "Password is required!";
                    }
                    else if (Routine.IsValueEmpty(_addViewModel.Created.ToString()))
                    {
                        _addViewModel.ErrorMessage = "Date is required!";

                    }
                    else
                    {

                        string passwordHasher = _passwordCryptor.Encrypt(_addViewModel.Password);

                        Service service = new Service
                        {
                            Title = _addViewModel.Title,
                            Username = _addViewModel.Username,
                            Password = passwordHasher,
                            Created = _addViewModel.Created,
                            Comment = _addViewModel.Comment,
                            Account = _accountStore.CurrentAccount
                        };
                        _unitOfWork.Services.Add(service);
                        _unitOfWork.Save();
                        _homeRenavigator.Renavigate();
                    }
                }
            }






        }

        private void AddViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AddViewModel.CanRegister))
            {
                OnCanExecuteChanged();
            }
        }


    }
}
