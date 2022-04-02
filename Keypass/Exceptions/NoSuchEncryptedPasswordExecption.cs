using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Keypass.Exceptions
{
    public class NoSuchEncryptedPasswordExecption : Exception
    {
        public NoSuchEncryptedPasswordExecption()
        {
        }

        public NoSuchEncryptedPasswordExecption(string message) : base(message)
        {
        }

        public NoSuchEncryptedPasswordExecption(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
