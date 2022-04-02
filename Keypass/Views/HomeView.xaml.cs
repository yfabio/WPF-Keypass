using Keypass.Domain;
using Keypass.ViewModels;
using Keypass.ViewModels.WindowServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Keypass.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public IHomeViewService IHomeViewService { get; set; }

        public HomeView()
        {
            InitializeComponent();
        }

        private void ListServices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IHomeViewService.SelectionChanged(ListServices.SelectedItem as Service);
            UnCheck();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            IHomeViewService = DataContext as HomeViewModel;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            IHomeViewService.Checked();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            IHomeViewService.Unchecked();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.OriginalSource is TextBox textBox && string.IsNullOrEmpty(textBox.Text))
            {
                IHomeViewService.SearchBoxEmptyEvent();
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (IHomeViewService != null)
            {
                if (e.OriginalSource is RadioButton radio)
                {
                    string content = radio.Content.ToString();
                    IHomeViewService.SelectedRadioBox(content);
                }
            }

        }

        public void UnCheck()
        {
            if (ShowPassword.IsChecked.Value)
            {
                ShowPassword.IsChecked = false;
            }
        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            Key key = e.Key;
            if (key == Key.Enter)
            {
                IHomeViewService.OnInputSearchHandler();
            }
        }


    }
}
