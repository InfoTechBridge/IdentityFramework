using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityFramework.Core.Providers
{
    public interface ISmsProvider
    {
        Task SendMessage(string message, string recipient);
    }
}
