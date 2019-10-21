using ORMToolkit.Core.Attributes;
using System;
using System.Collections.Generic;

namespace IdentityFramework.Core
{
    [Serializable()]
    public partial class GroupUser : ICloneable, IComparable<GroupUser>
    {
        [PrimaryKey]
        public string GroupName { get; set; }
        //public string UserName { get; set; }
        [PrimaryKey]
        public long UserId { get; set; }

        public GroupUser()
        {

        }

        public GroupUser(string groupname, long userId)
        {
			this.GroupName = groupname;
			this.UserId = userId;
        }
                
        public int CompareTo(GroupUser other)
        {
			return GroupName.CompareTo(other.GroupName) & UserId.CompareTo(other.UserId);
        }
                
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        
        public override string ToString()
        {
			return string.Format("{0}, {1}", GroupName, UserId);
        }        
      
    }
}
