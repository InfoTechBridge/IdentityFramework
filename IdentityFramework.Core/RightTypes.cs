using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityFramework.Core
{
    [Serializable()]
    public sealed class AdministrationRight : Rights
    {
        public AdministrationRight()
            : base("Administration")
        {
            FullName = Resx.AppResources.AdministrationRight;
            Description = "";
        }
    }

    [Serializable()]
    public sealed class LoginRight : Rights
    {
        public LoginRight()
            : base("Login")
        {
            FullName = Resx.AppResources.LoginRight;
            Description = "";
        }
    }

    [Serializable()]
    public sealed class RegisterNewUserRight : Rights
    {
        public RegisterNewUserRight()
            : base("RegisterNewUser")
        {
            FullName = Resx.AppResources.RegisterNewUserRight;
            Description = "";
        }
    }

    [Serializable()]
    public sealed class ChangePasswordRight : Rights
    {
        public ChangePasswordRight()
            : base("ChangePassword")
        {
            FullName = Resx.AppResources.ChangePasswordRight;
            Description = "";
        }
    }
}
