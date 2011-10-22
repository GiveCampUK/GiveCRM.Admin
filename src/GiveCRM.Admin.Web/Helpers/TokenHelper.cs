using System;
using System.Security.Cryptography;

namespace GiveCRM.Admin.Web.Helpers
{
    public class TokenHelper
    {
        private static readonly int RandomIdentifierSize = new Guid().ToByteArray().Length;

        public static Guid CreateRandomIdentifier()
        {
            var buff = GetRandomBytes(RandomIdentifierSize);
            return new Guid(buff);
        }

        private static byte[] GetRandomBytes(int numberOfBytes)
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[numberOfBytes];
            rng.GetBytes(buff);
            return buff;
        }
    }
}