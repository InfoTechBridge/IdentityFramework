using System;
using System.Collections.Generic;

namespace IdentityFramework.Core
{        
    [Serializable()]
    public partial class Application : ICloneable, IComparable<Application>
    {

        public string Name { get; set; }
        public string Title { get; set; }
        public string AppKey { get; set; }
        public short OtpAuthAlgorithm { get; set; }
        public short OtpAuthValueType { get; set; }
        public short OtpAuthValueLength { get; set; }
        public short OtpSigneAlgorithm { get; set; }
        public short OtpSigneValueType { get; set; }
        public short OtpSigneValueLength { get; set; }
        public bool SupportOtpByTime { get; set; }
        public bool SupportOtpByCounter { get; set; }
        public bool SupportOtpByChallenge { get; set; }
        public bool SupportDigitalSigne { get; set; }
        public string Description { get; set; }

        public Application()
        {
            Name = Guid.NewGuid().ToString();
        }

        public Application(string name)
        {
			this.Name = name;
        }
        
        public int CompareTo(Application other)
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
