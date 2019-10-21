using System;
using System.Collections.Generic;

namespace IdentityFramework.Core
{    
    [Serializable()]
    public partial class Rights : ICloneable, IComparable<Rights>
    {

        public string Name { get; set; }
        public string FullName { get; set; }
        public bool Disable { get; set; }
        public string Description { get; set; }

        public Rights(string name)
        {
            this.Name = name;
        }

        public int CompareTo(Rights other)
        {
            return Name.CompareTo(other.Name);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override string ToString()
        {
            return string.Format("{0}", Name);
        }
    }
}
