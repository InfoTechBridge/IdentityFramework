using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityFramework.Core
{
    public class GroupRightSummary : GroupRight
    {
        public string FullName { get; set; }
        public bool Disable { get; set; }
        public string Description { get; set; }
    }
}
