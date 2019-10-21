using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityFramework.Core.Providers
{
    public interface ICryptoProvider
    {
        string MD5(string value);
        string SHA1(string value);
        string HMACSHA1(string value, string salt);
    }
}
