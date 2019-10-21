using ORMToolkit.Core.Attributes;
using System;
using System.Collections.Generic;

namespace IdentityFramework.Core
{
    [Serializable()]
    public partial class UserRight : ICloneable, IComparable<UserRight>
    {
        [PrimaryKey]
        public long UserId { get; set; }
        //public string UserName { get; set; }
        [PrimaryKey]
        public string RightName { get; set; }
        [PrimaryKey]
        public string ResourceId { get; set; }
        public bool Status { get; set; }

        public UserRight()
        {

        }
        public UserRight(long userId, string rightname, string resourceid)
        {
            this.UserId = userId;
            this.RightName = rightname;
            this.ResourceId = resourceid;
        }

        public int CompareTo(UserRight other)
        {
            return UserId.CompareTo(other.UserId) & RightName.CompareTo(other.RightName) & ResourceId.CompareTo(other.ResourceId);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", UserId, RightName, ResourceId);
        }

    }
}
