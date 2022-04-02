using Keypass.Resources.Static;
using Keypass.Services.FileService;
using Keypass.ViewModels;
using Keypass.ViewModels.WindowServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Keypass.Commands
{
    public class ExportCommand : AsyncCommandBase
    {
        private readonly OptionViewModel optionView;
        private readonly IFileExport fileExport;


        public ExportCommand(OptionViewModel optionView,
            IFileExport fileExport)
        {
            this.optionView = optionView;
            this.fileExport = fileExport;
        }

        public override void ExecuteAsync(object parameter)
        {

            optionView.OptionViewService.ShowAndDisableExport();

            string dir = optionView.OptionViewService.GetExportFilePath();

            string extension = optionView.OptionViewService.GetSelectedExtension();

            Task.Run(() =>
            {
                switch (extension)
                {
                    case SD.JSON:
                        fileExport.ExportToJson(dir);
                        break;
                    case SD.XML:
                        fileExport.ExportToXml(dir);
                        break;
                    case SD.TXT:
                        fileExport.ExportToTxt(dir);
                        break;
                    default:
                        throw new ApplicationException("Unsupported Extension!");
                }

            }).ContinueWith(task =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    optionView.OptionViewService.HideAndEnableExport();
                    MessageDialog();
                });
            });
        }

        private void MessageDialog()
        {
            MessageBox.Show("Finished", "File Export", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

    }
}
