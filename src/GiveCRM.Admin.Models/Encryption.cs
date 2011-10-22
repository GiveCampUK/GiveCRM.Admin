using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveCRM.Admin.Models
{
    internal class Encryption
    {
        private static readonly byte[] SystemSalt = Guid.Parse("{1CAE651B-A885-4AD4-B7F0-0682CF8CA033}").ToByteArray();

        internal byte[] EncryptPassword(string plainText, byte[] salt)
        {
            using (var crypter = new System.Security.Cryptography.SHA512CryptoServiceProvider())
            {
                var bytes = Encoding.Default.GetBytes(plainText);
                var buffer = new byte[bytes.Length + salt.Length + SystemSalt.Length];
                bytes.CopyTo(buffer, 0);
                salt.CopyTo(buffer, bytes.Length);
                SystemSalt.CopyTo(buffer, bytes.Length + salt.Length);
                return crypter.ComputeHash(buffer);
            }
        }
    }
}
