using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityFramework.Core.Providers
{
    public class CryptoProvider
    {
        public static string MD5(string value)
        {
            byte[] inBuf = Encoding.UTF8.GetBytes(value);
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            var hash = md5.ComputeHash(inBuf);
            return OtpTools.ByteArrayToHexString(hash);

        }

        public static string SHA1(string value)
        {
            byte[] inBuf = Encoding.UTF8.GetBytes(value);
            System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1.Create();
            var hash = sha1.ComputeHash(inBuf);
            return OtpTools.ByteArrayToHexString(hash);
        }

        public static string HMACSHA1(string value, string salt)
        {
            byte[] inBuf = Encoding.UTF8.GetBytes(value);
            System.Security.Cryptography.HMACSHA1 sha1 = new System.Security.Cryptography.HMACSHA1(Encoding.UTF8.GetBytes(salt));
            var hash = sha1.ComputeHash(inBuf);
            return OtpTools.ByteArrayToHexString(hash);
        }
    }
}
