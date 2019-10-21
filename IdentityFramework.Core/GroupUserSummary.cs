using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityFramework.Core
{
    public class GroupUserSummary : GroupUser
    {
        public string FullName { get; set; }
        public string Parent { get; set; }
        public string Description { get; set; }
    }
}
