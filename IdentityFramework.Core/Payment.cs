using ORMToolkit.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityFramework.Core
{
    [Serializable()]
    [InsertReturning("Id")]
    public class Payment : ICloneable, IComparable<Payment>
    {
        [PrimaryKey]
        public long Id { get; set; }
        public long AccountNo { get; set; }
        public int PaymentProviderId { get; set; }
        public decimal Amount { get; set; }
        public decimal Vat { get; set; }
        public decimal Discount { get; set; }
        public decimal PayAmount { get; set; }
        public decimal Promotion { get; set; }
        public short PayType { get; set; }        
        public DateTimeOffset PayTime { get; set; }
        public string ProviderRefNo { get; set; }
        public string PayStatus { get; set; }
        public string PaySatusDesc { get; set; }
        public string VerifyStatus { get; set; }
        public string VerifyStatusDesc { get; set; }
        public decimal ReversedAmount { get; set; }
        public bool Auditing { get; set; }
        public short State { get; set; }
        public DateTimeOffset StateTime { get; set; }
        public DateTimeOffset CreateTime { get; set; }        
        public string Description { get; set; }

        public int CompareTo(Payment other)
        {
            return Id.CompareTo(other.Id);
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public override string ToString()
        {
            return string.Format("{0}", Amount);
        }
    }
}
