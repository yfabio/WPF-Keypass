using Keypass.ViewModels.WindowServices;
using MvvmFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keypass.ViewModels
{
    public class DialogPasswordViewModel : ViewModelBase
    {

        private string username;

        public string Username
        {
            get
            { return username; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    AddError("username is required!");
                }
                else 
                {
                    SetValue(ref username, value);
                    RemoveErrors();
                }
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set {
                if (string.IsNullOrEmpty(value))
                {
                    AddError("password is required!");
                }
                else
                {
                    SetValue(ref password, value);
                    RemoveErrors();
                }
            }
        }

        public IDialogViewService DiagoService { get; set; }


        private Command confirm;

        public Command Confirm
        {
            get => confirm;
        }


        public DialogPasswordViewModel()
        {
            confirm = new Command(OnConfirmHandler,false);

            ErrorsChanged += (s, a) => confirm.CanExecute = !HasErrors;
            
        }

        private void OnConfirmHandler()
        {
            DiagoService.Open();
        }
    }
}
