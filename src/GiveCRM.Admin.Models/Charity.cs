using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveCRM.Admin.Models
{
    public class Charity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string RegisteredCharityNumber { get; set; }
        public string SubDomain { get; set; }
        public byte[] EncryptedPassword { get; set; }
        public byte[] Salt { get; set; }

        public void SetPassword(string plainText)
        {
            Salt = Guid.NewGuid().ToByteArray();
            EncryptedPassword = EncryptPassword(plainText);
        }

        private byte[] EncryptPassword(string plainText)
        {
            return new Encryption().EncryptPassword(plainText, Salt);
        }

        public bool VerifyPassword(string password)
        {
            var encryptedCheck = EncryptPassword(password);
            return encryptedCheck.SequenceEqual(EncryptedPassword);
        }
    }
}
