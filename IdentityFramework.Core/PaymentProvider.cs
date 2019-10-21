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
    public class PaymentProvider : ICloneable, IComparable<PaymentProvider>
    {
        [PrimaryKey]
        public long Id { get; set; }
        public string Name { get; set; }
        public short ProviderType { get; set; }
        public bool Enable { get; set; }
        public string MerchantId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string GatewayUrl { get; set; }
        public string ProxyUrl { get; set; }
        public string ProxyUser { get; set; }
        public string ProxyPassword { get; set; }
        public bool AllowOnlinePay { get; set; }
        public bool AllowManualPay { get; set; }
        public bool Auditing { get; set; }
        public bool UseVatPlan { get; set; }
        public bool UseDiscountPlan { get; set; }
        public bool UsePromotionPlan { get; set; }
        public string UserRightName { get; set; }
        public string AccountPattern { get; set; }
        public string ExtraData { get; set; }        
        public string Description { get; set; }        

        public int CompareTo(PaymentProvider other)
        {
            return Id.CompareTo(other.Id);
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
