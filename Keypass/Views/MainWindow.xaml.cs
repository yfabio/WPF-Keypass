using Keypass.ViewModels;
using Keypass.ViewModels.WindowServices;
using System;
using System.Windows;

namespace Keypass.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IMainWindowService windowService;

        public MainWindow(object dataContext)
        {
            InitializeComponent();

            DataContext = dataContext;
        }

        
        private void Window_Closed(object sender, System.EventArgs e)
        {
            if(windowService!=null)
            windowService.OnClose();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainViewModel main = DataContext as MainViewModel;
            if (main!=null) 
            {
                windowService = main;
            }
        }
    }
}
