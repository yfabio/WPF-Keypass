using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keypass.Services.FileService
{
    public interface IFileExport
    {
        void ExportToXml(string dir);
        void ExportToJson(string dir);
        void ExportToTxt(string dir);
    }
}
