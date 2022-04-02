using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keypass.ViewModels
{
    public class MessageViewModel : ViewModelBase
    {
        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {               
                SetValue(ref _message, value);
                OnPropertyChanged(nameof(HasMessage));
            }
        }

        public bool HasMessage => !string.IsNullOrEmpty(Message);
    }
}
