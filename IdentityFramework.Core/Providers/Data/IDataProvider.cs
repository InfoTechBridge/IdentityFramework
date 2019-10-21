using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORMToolkit.Core;

namespace IdentityFramework.Core.Providers.Data
{
    public interface IDataProvider : IDataManager
    {
        IQueryable<User> Users { get; }
        IQueryable<Rights> Rights { get; }
        IQueryable<UserRight> UserRights { get; }
        IQueryable<Groups> Groups { get; }
        IQueryable<GroupUser> GroupUsers { get; }
        IQueryable<GroupRight> GroupRights { get; }

        bool UpdateUserLoginStatus(User user);
        bool UpdateUserPasswordStatus(User user);
        bool UpdateUserPasswordFailureCount(User user);
        bool UpdateUserPasswordAnswerFailureCount(User user);
        bool UpdateUserQuestionAndAnswer(User user);
        bool UpdateUserUnlock(User user);
    }
}
