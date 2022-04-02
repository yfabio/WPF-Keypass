using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keypass.Services.Criptography
{
    public interface IPasswordCryptor
    {
        string Encrypt(string password);
        string Decrypt(string password);
    }
}
