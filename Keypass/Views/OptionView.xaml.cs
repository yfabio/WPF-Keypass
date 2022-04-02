using Keypass.Resources.Static;
using Keypass.ViewModels;
using Keypass.ViewModels.WindowServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    /// Interaction logic for OptionView.xaml
    /// </summary>
    public partial class OptionView : UserControl, IOptionViewService
    {
        public OptionView()
        {
            InitializeComponent();
        }

        public void HideAndEnableExport()
        {
            exportProgress.Visibility = Visibility.Collapsed;
            importExpander.IsEnabled = true;
            exportFile.Text = "";           
            btnBrowser.IsEnabled = true;
        }

        public void ShowAndDisableExport()
        {
            exportProgress.Visibility = Visibility.Visible;
            importExpander.IsEnabled = false;            
            btnBrowser.IsEnabled = false;
        }

        public void HideAndEnableImport()
        {
            importProgress.Visibility = Visibility.Collapsed;
            exportExpander.IsEnabled = true;
            importFile.Text = "";            
        }

        public void ShowAndDisableImport()
        {
            importProgress.Visibility = Visibility.Visible;
            exportExpander.IsEnabled = false;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            OptionViewModel optionView = DataContext as OptionViewModel;

            if (optionView != null)
            {
                optionView.OptionViewService = this;
            }
        }

        public void ShowDialog(string key)
        {
           
            if (key == SD.IMPORT_FILE)
            {
                var fileDialog = new Microsoft.Win32.OpenFileDialog();
                fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

                if (fileDialog.ShowDialog().HasValue)
                {
                    importFile.Text = fileDialog.FileName;
                }
            }
            else if (key == SD.EXPORT_FILE)
            {
                var folderDialog = new System.Windows.Forms.FolderBrowserDialog();
                
                if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    exportFile.Text = folderDialog.SelectedPath;
                }
            }
            else
            {
                throw new ArgumentException("invalid key!");
            }

        }

        public string GetExportFilePath()
        {
            return exportFile.Text;
        }

        public string GetImportFilePath()
        {
            return importFile.Text;
        }

        public string GetSelectedExtension()
        {
            if (btnJson.IsChecked.Value)
            {
                return SD.JSON;
            }
            else if (btnXml.IsChecked.Value)
            {
                return SD.XML;
            }
            else if (btnTxt.IsChecked.Value)
            {
                return SD.TXT;
            }
            else 
            {
                throw new ArgumentException("Invalid radio button selected");
            }
        }

        public string GetFullPath()
        {
            return importFile.Text;
        }
    }



}
