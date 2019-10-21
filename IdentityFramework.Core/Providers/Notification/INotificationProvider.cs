using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityFramework.Core.Providers.Notification
{
    public interface INotificationProvider
    {
        Task SendEmailVerificationMessage(string email, string recipientName, string link, string appTitle);
        Task SendPhoneVerificationMessage(string phone, string recipientName, string code, string appTitle);

        Task SendInvitationEmailMessage(string email, string link, string appTitle);
        Task SendInvitationPhoneMessage(string phone, string link, string appTitle);
    }
}
