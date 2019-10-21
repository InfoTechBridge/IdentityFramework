using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityFramework.Core
{
    public enum AuthCodeMessageType
    {
        SmsMessageWithCode = 0,
        SmsMessageWithAppLink = 1,
        ChatMessage = 2,
        PhoneCall = 3,
        Email = 4,
        PushMessage = 5,
    }
}
