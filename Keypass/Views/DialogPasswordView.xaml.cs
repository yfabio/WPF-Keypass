using Keypass.ViewModels;
using Keypass.ViewModels.WindowServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Keypass.Views
{
    /// <summary>
    /// Interaction logic for PasswordDialog.xaml
    /// </summary>
    public partial class DialogPasswordView : Window, IDialogViewService
    {
        public DialogPasswordView()
        {
            InitializeComponent();
        }

        public event Func<bool> IsSucceed;

        public void Open()
        {
            this.DialogResult = IsSucceed.Invoke(); 
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            usernameBox.Focus();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DialogPasswordViewModel dialogPassword = DataContext as DialogPasswordViewModel;
            if (dialogPassword != null)
            {
                dialogPassword = (DialogPasswordViewModel)DataContext;
                dialogPassword.DiagoService = this;
            }
        }
    }
}
