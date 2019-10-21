using ORMToolkit.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityFramework.Core
{
    [Serializable()]
    [InsertReturning("No")]
    public class Account : ICloneable, IComparable<Account>
    {
        [PrimaryKey]
        public long No { get; set; }
        public long? ParrentAccountNo { get; set; }
        public long UserId { get; set; }
        public decimal TotalDebitBalance { get; set; }
        public decimal TotalCreditBalance { get; set; }
        public decimal TotalBalance { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset? UpdateTime { get; set; }

        
        public int CompareTo(Account other)
        {
            return No.CompareTo(other.No);
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public override string ToString()
        {
            return string.Format("{0}", No);
        }

    }
}
