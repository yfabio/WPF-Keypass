using Keypass.Domain;
using Keypass.Repository.Interfaces;
using Keypass.Resources.Static;
using Keypass.Services.Criptography;
using Keypass.Services.FileService;
using Keypass.State.Navigators;
using Keypass.ViewModels;
using Keypass.Views;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Keypass.Commands
{
    public class ImportCommand : AsyncCommandBase
    {
        private OptionViewModel optionView;
        private IFileImport fileImport;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPasswordHasher passwordHasher;
        private DialogPasswordView passwordView;

        public ImportCommand(OptionViewModel optionView,
            IFileImport fileImport,
            IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher
           )
        {
            this.optionView = optionView;
            this.fileImport = fileImport;
            this.unitOfWork = unitOfWork;
            this.passwordHasher = passwordHasher;
        }


        public override void ExecuteAsync(object parameter)
        {
            passwordView = new DialogPasswordView();
            passwordView.IsSucceed += DialogPasswordConfirmation;
            optionView.ErrorMessage = "";

            if (passwordView.ShowDialog() == true)
            {
                string fullpath = optionView.OptionViewService.GetFullPath();

                string extension = fullpath.Substring(fullpath.LastIndexOf(".") + 1);

                Task task = Task.Run(() =>
                {
                    switch (extension)
                    {
                        case SD.JSON:
                            ShowAndDisableImport();
                            fileImport.ImportToJson(fullpath);
                            break;
                        case SD.XML:
                            ShowAndDisableImport();
                            fileImport.ImportToXml(fullpath);
                            break;
                        case SD.TXT:
                            ShowAndDisableImport();
                            fileImport.ImportToTxt(fullpath);
                            break;
                        default:
                            throw new ArgumentException("Unsupported Extention!");
                    }

                }).ContinueWith(task =>
                {
                    if (task.Exception != null)
                    {
                        AggregateException exception = task.Exception as AggregateException;

                        StringBuilder sb = new StringBuilder();

                        foreach (AggregateException ex in exception.InnerExceptions)
                        {
                            sb.Append(ex.Message);
                        }

                        Application.Current.Dispatcher.Invoke(() => {
                            optionView.ErrorMessage = sb.ToString();
                        });
                    }
                    else 
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            passwordView.IsSucceed -= DialogPasswordConfirmation;
                            optionView.OptionViewService.HideAndEnableImport();
                            MessageDialog();
                        });
                    }
                });
            }
        }

        private bool DialogPasswordConfirmation()
        {
            string password = passwordView.passwordBox.Password;
            string username = passwordView.usernameBox.Text;

            Account account = unitOfWork.Accounts.GetByUsername(username);
            PasswordVerificationResult passwordResult = PasswordVerificationResult.Failed;

            try
            {
                passwordResult = passwordHasher.VerifyHashedPassword(account.Password, password);
            }
            catch (Exception)
            {
                return false;
            }

            if (passwordResult == PasswordVerificationResult.Success)
            {
                return true;
            }

            return false;
        }

        private void ShowAndDisableImport() 
        {
            Application.Current.Dispatcher.Invoke(() => {
                optionView.OptionViewService.ShowAndDisableImport();
            });
        }


        private void MessageDialog()
        {
            MessageBox.Show("Finished", "File Import", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }





    }
}
