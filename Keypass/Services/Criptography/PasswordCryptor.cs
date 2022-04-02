using Keypass.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Keypass.Services.Criptography
{
    public class PasswordCryptor :IPasswordCryptor
    {
        private const string PublicKey = "Key%Pass";
        private const string PrivateKey = "Key$pass";

        public string Decrypt(string password)
        {
            try
            {
                byte[] passwordBase64 = DecodeBase64(password);
                MemoryStream ms = new MemoryStream();

                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(GetPublicKey(), GetPrivateKey()), CryptoStreamMode.Write);
                    cs.Write(passwordBase64, 0, passwordBase64.Length);
                    cs.FlushFinalBlock();
                }

                return UTF8Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (Exception e)
            {
                throw new NoSuchEncryptedPasswordExecption("Password was not encrypted",e);
            }
        }

        public string Encrypt(string password)
        {
            byte[] passwordByte = UTF8Encoding.UTF8.GetBytes(password);
            MemoryStream ms = new MemoryStream();

            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(GetPublicKey(), GetPrivateKey()), CryptoStreamMode.Write);
                cs.Write(passwordByte, 0, passwordByte.Length);
                cs.FlushFinalBlock();
            }

            return EncodeBase64(ms.ToArray());
        }

        private byte[] GetPublicKey()
        {
            return UTF8Encoding.UTF8.GetBytes(PublicKey);
        }

        private byte[] GetPrivateKey()
        {
            return UTF8Encoding.UTF8.GetBytes(PrivateKey);
        }

        private string EncodeBase64(byte[] value)
        {
            return Convert.ToBase64String(value);
        }

        private byte[] DecodeBase64(string value)
        {
            return Convert.FromBase64String(value);
        }

    }
}
