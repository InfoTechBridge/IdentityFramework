using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace IdentityFramework.Core
{
    [Serializable()]
    public partial class EventLog : ICloneable, IComparable<EventLog>
    {

        public long Id { get; set; }
        public EventLogType Type { get; set; }
        public DateTime EventDae { get; set; }
        public string UserName { get; set; }
        public string RightName { get; set; }
        public string ResourceId { get; set; }
        public string ClientInfo { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string Description { get; set; }

        public string TypeCaption
        {
            get
            {
                
                EventLogType value = (EventLogType)this.Type;
                FieldInfo fi = value.GetType().GetField(value.ToString());
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
            }
        }

        public EventLog(long id)
        {
			this.Id = id;
        }
        
       public int CompareTo(EventLog other)
        {
			return Id.CompareTo(other.Id);
        }
        
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        
        public override string ToString()
        {
			return string.Format("{0}", Id);
        }       
               
    }
}
