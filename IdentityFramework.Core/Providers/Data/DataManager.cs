using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using ORMToolkit.Core;
using ORMToolkit.Core.Configuration;
using ORMToolkit.Core.Factories;
using ORMToolkit.Core.Repository;
using ORMToolkit.Linq;

namespace IdentityFramework.Core.Providers.Data
{
    public class DataManager : DataManagerNoConfilict, IDataProvider
    {
        //public DbRepository<User> Users { get; set; }
        public IQueryable<User> Users { get; private set; }
        public IQueryable<Rights> Rights { get; private set; }
        public IQueryable<UserRight> UserRights { get; private set; }
        public IQueryable<Groups> Groups { get; private set; }
        public IQueryable<GroupUser> GroupUsers { get; private set; }
        public IQueryable<GroupRight> GroupRights { get; private set; }

        //public DataManager()
        //    //: base(new Oracle.DataAccess.Client.OracleConnection(ConfigurationManager.ConnectionStrings["oracleDb"].ConnectionString))
        //    //: base(ConfigurationManager.ConnectionStrings["oracleDbManaged"].ConnectionString, new ORMToolkit.OracleOdpManaged.OracleOdpManagedDataProviderFactory())
        //    : base("User Id=chatbox;Password=chatbox;Data Source=XE;", new ORMToolkit.OracleOdpManaged.OracleOdpManagedDataProviderFactory())
        ////: base("oracleDb")
        //{
        //    //var connStr = ConfigurationManager.ConnectionStrings["oracleDbManaged"].ConnectionString;
        //    //var conn = new Oracle.DataAccess.Client.OracleConnection(connStr);
        //    ORMToolkit.Core.OrmToolkitSettings.RegisterFactory(new ORMToolkit.OracleOdpManaged.OracleOdpManagedDataProviderFactory());
        //    OrmToolkitSettings.ObjectFactory = new ORMToolkit.Core.Factories.ObjectFactory2();
        //    //OrmToolkitSettings.CacheCommands = false;
        //}

        public DataManager(string connectionString, Factory factory)
            : base(connectionString, factory)
        {
            Init();
        }

        public DataManager(DbConnection connection)
            : base(connection)
        {
            Init();
        }

#if NET45
        public DataManager(string connectionStringName)
            : base(connectionStringName)
        {
            Init();
        }
#endif
        public DataManager(DataManagerConfiguration configuration)
            : base(configuration)
        {
            Init();
        }

        private void Init()
        {
            //Users = new DbRepository<BusinessObjects.Users>(base.co)
            IQueryProvider provider = new DbQueryProvider(Connection);
            Users = new MyQueryable<User>(provider);
            Rights = new MyQueryable<Rights>(provider);
            UserRights = new MyQueryable<UserRight>(provider);
            Groups = new MyQueryable<Groups>(provider);
            GroupUsers = new MyQueryable<GroupUser>(provider);
            GroupRights = new MyQueryable<GroupRight>(provider);
        }


        public bool UpdateUserLoginStatus(User user)
        {
            var updateInfo = new
            {
                IsOnline = user.IsOnline,
                LastLoginTime = user.LastLoginTime,
                LastActivityTime = user.LastActivityTime
            };

            var count = Update<User>(updateInfo, new { Name = user.Name });

            return count == 1;
        }

        public bool UpdateUserPasswordStatus(User user)
        {
            var updateInfo = new
            {
                Password = user.Password,
                LastPasswordChangedTime = user.LastPasswordChangeTime,
                MustChangePassword = user.MustChangePassword,
                PasswordExpireTime = user.PasswordExpireTime
            };
            var count = Update<User>(updateInfo, new { Name = user.Name });

            return count == 1;
        }

        public bool UpdateUserPasswordFailureCount(User user)
        {
            var updateInfo = new
            {
                FailedPswAttemptCount = user.FailedPswAttemptCount,
                FailedPswAttemptWinStart = user.FailedPswAttemptWinStart,
                IsLockedOut = user.IsLockedOut,
                LastLockedOutTime = user.LastLockedOutTime
            };
            var count = Update<User>(updateInfo, new { Name = user.Name });

            return count == 1;
        }

        public bool UpdateUserPasswordAnswerFailureCount(User user)
        {
            var updateInfo = new
            {
                FailedPswAnswerAttemptCount = user.FailedPswAnswerAttemptCount,
                FailedPswAnswerAttemptWinStart = user.FailedPswAnswerAttemptWinStart,
                IsLockedOut = user.IsLockedOut,
                LastLockedOutTime = user.LastLockedOutTime
            };
            var count = Update<User>(updateInfo, new { Name = user.Name });

            return count == 1;
        }
        public bool UpdateUserQuestionAndAnswer(User user)
        {
            var updateInfo = new
            {
                PasswordQuestion = user.PasswordQuestion,
                PasswordAnswer = user.PasswordAnswer,
            };
            var count = Update<User>(updateInfo, new { Name = user.Name });

            return count == 1;
        }
        public bool UpdateUserUnlock(User user)
        {
            var updateInfo = new
            {
                IsLockedOut = user.IsLockedOut,
                FailedPswAttemptCount = 0,
            };
            var count = Update<User>(updateInfo, new { Name = user.Name });

            return count == 1;
        }
    }
}
