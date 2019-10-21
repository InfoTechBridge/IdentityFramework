using System;
using System.Collections.Generic;

namespace IdentityFramework.Core
{
    [Serializable()]
    public partial class Groups : ICloneable, IComparable<Groups>
    {

        public string Name { get; set; }
        public string FullName { get; set; }
        public string Parent { get; set; }
        public string Description { get; set; }

        public Groups(string name)
        {
            this.Name = name;
        }

        public int CompareTo(Groups other)
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
