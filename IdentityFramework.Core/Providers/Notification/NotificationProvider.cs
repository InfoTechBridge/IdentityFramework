using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityFramework.Core.Providers.Notification
{
    public class NotificationProvider : INotificationProvider
    {
        private readonly ISmsProvider _smsProvider;
        private readonly IEmailProvider _emailProvider;

        public NotificationProvider(ISmsProvider smsProvider, IEmailProvider emailProvider)
        {
            _smsProvider = smsProvider;
            _emailProvider = emailProvider;
        }

        public virtual async Task SendEmailVerificationMessage(string email, string recipientName, string link, string appTitle)
        {           
            await _emailProvider?.SendMessage(email,
                string.Format(Resx.AppResources.UserActivationEmailSubject, appTitle),
                string.Format(Resx.AppResources.UserActivationEmailBody, recipientName, link, appTitle)
                );
        }

        public virtual async Task SendPhoneVerificationMessage(string phone, string recipientName, string code, string appTitle)
        {
            await _smsProvider?.SendMessage(string.Format(Resx.AppResources.PhoneVerificationSmsBody, appTitle, code), phone);
        }

        public virtual async Task SendInvitationEmailMessage(string email, string link, string appTitle)
        {
            await _emailProvider?.SendMessage(email,
                string.Format(Resx.AppResources.EmailInvitationSubject, appTitle),
                string.Format(Resx.AppResources.EmailInvitationBody, link, appTitle)
                );
        }

        public virtual async Task SendInvitationPhoneMessage(string phone, string link, string appTitle)
        {
            await _smsProvider?.SendMessage(string.Format(Resx.AppResources.PhoneInvitationSmsBody, appTitle, link), phone);
        }
    }
}
