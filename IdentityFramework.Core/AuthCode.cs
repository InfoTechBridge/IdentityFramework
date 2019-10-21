using ORMToolkit.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityFramework.Core
{
    [InsertReturning("Id")]
    public class AuthCode
    {
        [PrimaryKey]
        [AutoGenerate]
        public long Id { get; set; }
        public string Recipient { get; set; }
        public bool IsRegistered { get; set; }
        public AuthCodeMessageType MessageType { get; set; }
        public string CodeHash { get; set; }
        public bool IsPassword { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset ExpieryTime { get; set; }
        public DateTimeOffset? UsedTime { get; set; }
    }
}
