using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityFramework.Core.Providers
{
    public interface IEmailProvider
    {
        Task SendMessage(string to, string subject, string body, bool isBodyHtml = false);
        Task SendMessage(string from, string to, string subject, string body, bool isBodyHtml = false);
        Task SendMessage(string from, string senderName, string to, string subject, string body, bool isBodyHtml = false);
        Task SendMessage(string from, string to, string cc, string bcc, string subject, string body, bool isBodyHtml = false);
        Task SendMessage(string from, string senderName, string to, string cc, string bcc, string subject, string body, bool isBodyHtml = false);
    }
}
