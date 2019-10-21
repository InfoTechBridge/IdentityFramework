using IdentityFramework.Core.Providers.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityFramework.Core.Services
{
    public class AuthorizationService
    {
        private readonly IDataProvider _dataManager;

        public AuthorizationService(IDataProvider dataManager)
        {
            _dataManager = dataManager;
        }
               
        public bool IsPermitted(string username, string rightName, string resource = "*")
        {
            var user = _dataManager.Get<User>(new { Name = username });
            if (user == null)
                return false;// throw new UserRightException(Resx.AppResources.UserNotExistsException);

            return IsPermitted(user.Id, rightName, resource);
        }

        public bool IsPermitted(long userId, string rightName, string resource = "*")
        {
            bool RetValue = false;

            var ur = _dataManager.Get<UserRight>(new { UserId = userId, RightName = rightName, ResourceId = resource });
            if (ur != null)
                RetValue = ur.Status;
            else
            {
                ur = _dataManager.Get<UserRight>(new { UserId = userId, RightName = new AdministrationRight().Name, ResourceId = resource });
                if (ur != null)
                    RetValue = ur.Status;
                else // check group permition
                {
                    var groups = _dataManager.GetAll<GroupUser>(new { UserId = userId });
                    foreach (GroupUser gu in groups)
                    {
                        if (IsGroupPermitted(gu.GroupName, rightName, resource))
                        {
                            RetValue = true;
                            break;
                        }
                    }
                }
            }

            return RetValue;
        }
            

        public bool IsGroupPermitted(string groupName, string rightName, string resource = "*")
        {
            bool RetValue = false;

            var gr = _dataManager.Get<GroupRight>(new { GroupName = groupName, RightName = rightName, ResourceId = resource });
            if (gr != null)
                RetValue = gr.Status;
            else
            {
                gr = _dataManager.Get<GroupRight>(new { GroupName = groupName, RightName = new AdministrationRight().Name, ResourceId = resource });
                if (gr != null)
                    RetValue = gr.Status;
            }

            return RetValue;
        }

        public UserRight AddUserRight(long userId, string rightName, string resource, bool status)
        {
            var userRight = new UserRight
            {
                UserId = userId,
                RightName = rightName,
                ResourceId = resource,
                Status = status
            };
            _dataManager.Insert<UserRight>(userRight);

            return userRight;
        }

        public GroupRight AddGroupRight(string groupName, string rightName, string resource, bool status)
        {
            var groupRight = new GroupRight
            {
                GroupName = groupName,
                RightName = rightName,
                ResourceId = resource,
                Status = status
            };
            _dataManager.Insert<GroupRight>(groupRight);

            return groupRight;
        }
    }
}
