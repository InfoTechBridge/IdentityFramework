using System;
using System.Collections.Generic;

namespace IdentityFramework.Core
{    
    [Serializable()]
    public partial class Profile : ICloneable, IComparable<Profile>
    {

        public string UserName { get; set; }

        public bool IsAnonymous { get; set; }

        public DateTime LastActivityDate { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        public Profile(string username)
        {
			this.UserName = username;
        }        
                      
        public int CompareTo(Profile other)
        {
			return UserName.CompareTo(other.UserName);
        }
                        public object Clone()
        {
            return this.MemberwiseClone();
        }
        
        public override string ToString()
        {
			return string.Format("{0}", UserName);
        }
      
    }
}
