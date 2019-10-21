using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityFramework.Core
{
    public enum PasswordFormatType
    {        
        Clear = 0,
        Hashed = 1,
        Encrypted = 2,
        Otp = 3,
        OtpMessage = 4,
    }
}
