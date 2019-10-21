using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdentityFramework.Core
{
    [Serializable]
    public class UserRightException : Exception
    {
        const string _Message = "Application User Rights Exception.";
        
        //HRESULT is a 32-bit value, divided into three different fields: a severity code, a facility code, and an error code.
        //The severity code indicates whether the return value represents information, warning, or error.
        //The facility code identifies the area of the system responsible for the error.
        //The error code is a unique number that is assigned to represent the exception. 
        const int _HResult = unchecked((int)0x81234567);

        public UserRightException()
            : base(_Message)
        {
            Init();
        }

        public UserRightException(string auxMessage)
            : base(auxMessage)
        { Init(); }

        public UserRightException(string auxMessage, Exception inner)
            : base(auxMessage, inner)
        { Init(); }

        public UserRightException(Exception inner)
            : base(_Message, inner)
        { Init(); }

        private void Init()
        {
            this.HResult = _HResult;
            //this.HelpLink = "http://msdn.microsoft.com";
            //this.Source = "Application_User_Rights";

            //this.Data["IntInfo"] = i;
            //this.Data["DateTimeInfo"] = DateTime.Now;
        }

        public virtual int ErrorCode
        {
            get
            {
                return 1000;
            }
        }
    }

    [Serializable]
    public class PasswordExpireException : UserRightException
    {
        public PasswordExpireException()
            : base(Resx.AppResources.PasswordExpireException)
        { }

        public PasswordExpireException(string auxMessage)
            : base(auxMessage)
        { }

        public PasswordExpireException(string auxMessage, Exception inner)
            : base(auxMessage, inner)
        { }
    }

    [Serializable]
    public class ChangePasswordNextLoginException : UserRightException
    {
        public ChangePasswordNextLoginException()
            : base(Resx.AppResources.ChangePasswordNextLoginException)
        { }

        public ChangePasswordNextLoginException(string auxMessage)
            : base(auxMessage)
        { }

        public ChangePasswordNextLoginException(string auxMessage, Exception inner)
            : base(auxMessage, inner)
        { }
    }

    [Serializable]
    public class AccessDeniedException : UserRightException
    {
        private static string DefaultMessage = Resx.AppResources.AccessDeniedException;
        public Rights Right { get; private set; }
        
        public AccessDeniedException(Rights right)
            : base(string.Format(DefaultMessage, right.FullName))
        {
            
        }
        
        public AccessDeniedException(Rights right, string auxMessage)
            : base(auxMessage)
        {
            Right = right;
        }

        public AccessDeniedException(Rights right, string auxMessage, Exception inner)
            : base(auxMessage, inner)
        {
            Right = right;
        }

        public AccessDeniedException(Rights right, Exception inner)
            : base(string.Format(DefaultMessage, right.FullName), inner)
        {
            Right = right;
        }

        
    }

    [Serializable]
    public class InvalidUserNameOrPasswordException : UserRightException
    {
        public InvalidUserNameOrPasswordException()
            : base(Resx.AppResources.InvalidUserNameOrPasswordExceptin)
        { }

        public InvalidUserNameOrPasswordException(string auxMessage)
            : base(auxMessage)
        { }

        public InvalidUserNameOrPasswordException(string auxMessage, Exception inner)
            : base(auxMessage, inner)
        { }
    }

    public class ApplicationNotFoundException : UserRightException
    {
        public ApplicationNotFoundException()
            : base(Resx.AppResources.ApplicationNotFoundException)
        { }

        public ApplicationNotFoundException(string auxMessage)
            : base(auxMessage)
        { }

        public ApplicationNotFoundException(string auxMessage, Exception inner)
            : base(auxMessage, inner)
        { }
    }
}
