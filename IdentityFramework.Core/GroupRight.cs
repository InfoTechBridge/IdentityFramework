using System;
using System.Collections.Generic;

namespace IdentityFramework.Core
{
    
    [Serializable()]
    public partial class GroupRight : ICloneable, IComparable<GroupRight>
    {

        public string GroupName { get; set; }
        public string RightName { get; set; }
        public string ResourceId { get; set; }
        public bool Status { get; set; }

        public GroupRight()
        {

        }
        public GroupRight(string groupname, string rightname, string resourceid)
        {
			this.GroupName = groupname;
			this.RightName = rightname;
			this.ResourceId = resourceid;
        }

        public int CompareTo(GroupRight other)
        {
			return GroupName.CompareTo(other.GroupName) & RightName.CompareTo(other.RightName) & ResourceId.CompareTo(other.ResourceId);
        }
                
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        
        public override string ToString()
        {
			return string.Format("{0}, {1}, {2}", GroupName, RightName, ResourceId);
        }       
        
    }
}
