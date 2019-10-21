using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityFramework.Core
{
    public class UserRightStatus
    {
        public long? UserId { get; set; }
        public string RightName { get; set; }
        public string RightTitle { get; set; }
        public bool? Status { get; set; }
    }
}
