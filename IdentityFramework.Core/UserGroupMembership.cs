using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityFramework.Core
{
    public class UserGroupMembership
    {
        public long? UserId { get; set; }
        public string GroupName { get; set; }
        public string GroupTitle { get; set; }
        public string ParentGroup { get; set; }
        public bool Membership { get; set; }

    }
}
