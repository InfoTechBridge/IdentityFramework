
using IdentityFramework.Core.Providers;
using ORMToolkit.Core.Attributes;
using System;
    using System.Collections.Generic;
using System.Linq;

namespace IdentityFramework.Core
{
    [Serializable()]
    [TableInfo(tableName: "Users")]
    [InsertReturning("Id")]
    public partial class User : ICloneable, IComparable<User>
    {
        [AutoGenerate]
        public long Id { get; set; }
        [PrimaryKey]
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }        
        public string Password { get; set; }
        public PasswordFormatType PasswordFormat { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordQuestion { get; set; }
        public string PasswordAnswer { get; set; }
        public bool IsApproved { get; set; }
        public bool AllowChangePassword { get; set; }
        public bool MustChangePassword { get; set; }
        public bool IsOnline { get; set; }
        public bool IsLockedOut { get; set; }        
        public string UserKey { get; set; }
        public decimal Counter { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset? LastLoginTime { get; set; }
        public DateTimeOffset LastActivityTime { get; set; }
        public DateTimeOffset? LastLockedOutTime { get; set; }
        public DateTimeOffset? PasswordExpireTime { get; set; }
        public DateTimeOffset LastPasswordChangeTime { get; set; }
        public int FailedPswAttemptCount { get; set; }
        public DateTimeOffset FailedPswAttemptWinStart { get; set; }
        public int FailedPswAnswerAttemptCount { get; set; }
        public DateTimeOffset FailedPswAnswerAttemptWinStart { get; set; }
        public string RemoteClients { get; set; }
        public string AppName { get; set; }
        public string Description { get; set; }

        [NoInsert]
        public bool IsPasswordExpired
        {
            get
            {
                bool PswExpire = false;

                if (this.PasswordExpireTime != null
                    && DateTime.Now > this.PasswordExpireTime)
                    PswExpire = true;
                return PswExpire;
            }
        }

        [NoInsert]
        public string DisplayName
        {
            get
            {
                var userDisplayName = $"{FirstName} {LastName}".Trim();
                if (string.IsNullOrWhiteSpace(userDisplayName))
                    userDisplayName = Name;
                return userDisplayName;
            }
        }

        public User()
        {
            Name = Guid.NewGuid().ToString();

            Init();
        }

        public User(string name)
        {
            this.Name = name;

            Init();
        }

        public User(string name, string firstName, string lastName, string phone, string email)
        {
            Name = name;
            FirstName = firstName;
            LastName = LastName;
            Email = email;

            Init();
        }

        private void Init()
        {
            DateTimeOffset createTime = DateTimeOffset.UtcNow;
            CreateTime = createTime;
            LastPasswordChangeTime = createTime;
            LastActivityTime = createTime;
            LastLockedOutTime = createTime;
            FailedPswAttemptWinStart = createTime;
            FailedPswAnswerAttemptWinStart = createTime;
            AllowChangePassword = true;
        }
        
        public int CompareTo(User other)
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
