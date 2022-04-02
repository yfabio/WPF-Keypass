using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keypass.ViewModels.WindowServices
{
    public interface IOptionViewService
    {
        void ShowAndDisableExport();
        void HideAndEnableExport();

        void ShowAndDisableImport();
        void HideAndEnableImport();

        void ShowDialog(string key);
        string GetExportFilePath();
        string GetImportFilePath();
        string GetSelectedExtension();
        string GetFullPath();

    }
}
