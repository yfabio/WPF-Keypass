using Keypass.ViewModels.WindowServices;
using MvvmFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Timers;
using System.Threading;
using Timer = System.Timers.Timer;
using Microsoft.Win32;
using System.IO;
using System.Threading.Tasks;
using Keypass.Services.FileService;
using Keypass.Resources.Static;
using System.Windows.Input;
using Keypass.Commands;
using Keypass.State.Navigators;
using Keypass.Repository.Interfaces;
using Keypass.Services.Criptography;
using Microsoft.AspNet.Identity;

namespace Keypass.ViewModels
{
    public class OptionViewModel : ViewModelBase
    {
        public IOptionViewService OptionViewService { get; set; }

        public ICommand ExportCommand { get; }
        public ICommand ImportCommand { get; }

        public Command exportBrowseCommand;

        public Command ExportBrowseCommand
        {
            get => exportBrowseCommand;
        }

        public Command importBrowseCommand;
        private readonly IFileImport fileImport;
        private readonly IFileExport fileExport;


        public Command ImportBrowseCommand
        {
            get => importBrowseCommand;
        }

        public MessageViewModel ErrorMessageViewModel { get; }

        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }


        public OptionViewModel(IFileImport fileImport,
            IFileExport fileExport,IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher)
        {

            ErrorMessageViewModel = new MessageViewModel();

            importBrowseCommand = new Command(OnImportBrowseHandler);
            exportBrowseCommand = new Command(OnExportBrowseHandler);

            this.fileImport = fileImport;
            this.fileExport = fileExport;

            ExportCommand = new ExportCommand(this, fileExport);
            ImportCommand = new ImportCommand(this, fileImport, unitOfWork, passwordHasher);
           

        }

        private void OnExportBrowseHandler()
        {
            OptionViewService.ShowDialog("export");
        }

        private void OnImportBrowseHandler()
        {
            OptionViewService.ShowDialog("import");
        }


        public override void Dispose()
        {
            ErrorMessageViewModel.Dispose();
            base.Dispose();
        }


    }
}
