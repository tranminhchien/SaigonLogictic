using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Common.Utils
{
    public interface IEncryptionUtil
    {
        string EncodeSHA1(string pass);
    }

    public class EncryptionUtil : IEncryptionUtil
    {
        public string EncodeSHA1(string pass)
        {

            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

            var bs = sha1.ComputeHash(Encoding.UTF8.GetBytes(pass));

            StringBuilder s = new StringBuilder();

            foreach (byte b in bs)
            {
                s.Append(b.ToString("x1").ToLower());
            }

            return s.ToString();
        }
    }
}
