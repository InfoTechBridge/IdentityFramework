using System;
using System.Collections.Generic;

namespace IdentityFramework.Core
{
    [Serializable()]
    public partial class UserMessage : ICloneable, IComparable<UserMessage>
    {

        public long Id { get; set; }
        public string UserName { get; set; }
        public string GroupName { get; set; }
        public string Body { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public UserMessagePriority Priority { get; set; }
        public bool Disable { get; set; }
        public DateTime CreateTime { get; set; }

        public string PriorityCaption
        {
            get
            {
                return Common.GetEnumDescription(Priority);
            }
        }

        public UserMessage(long id)
        {
            this.Id = id;
        }
        public int CompareTo(UserMessage other)
        {
            return Id.CompareTo(other.Id);
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", Id, Body);
        }

    }
}
