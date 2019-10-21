using System;
using System.Collections.Generic;

namespace IdentityFramework.Core
{
    [Serializable()]
    public partial class ProfileProperty : ICloneable, IComparable<ProfileProperty>
    {
        public string UserName { get; set; }
        public string PropertyName { get; set; }
        public string PropertyCaption { get; set; }
        public string PropertyStringValue { get; set; }
        public byte[] PropertyBinaryValue { get; set; }

        public ProfileProperty(string username, string propertyname)
        {
            this.UserName = username;
            this.PropertyName = propertyname;
        }

        public int CompareTo(ProfileProperty other)
        {
            return UserName.CompareTo(other.UserName) & PropertyName.CompareTo(other.PropertyName);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public override string ToString()
        {
            return string.Format("{0}, {1}", UserName, PropertyName);
        }
    }
}
