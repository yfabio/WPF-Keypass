using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keypass.Services.FileService
{
    public interface IFileImport
    {
        void ImportToXml(string file);
        void ImportToJson(string file);
        void ImportToTxt(string file);
    }
}
