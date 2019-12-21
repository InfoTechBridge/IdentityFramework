using IdentityFramework.Core.Providers;
using IdentityFramework.Core.Providers.Data;
using IdentityFramework.Core.Providers.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace IdentityFramework.Core.Services
{
    public class AuthenticationService
    {
        public int NewPasswordLength { get; set; } = 8;
        public bool EnablePasswordReset { get; set; } = true;
        public bool EnablePasswordRetrieval { get; set; } = true;
        public bool RequiresQuestionAndAnswer { get; set; } = false;
        public bool RequiresUniqueEmail { get; set; } = true;
        public bool RequiresUniquePhone { get; set; } = true;
        public int MaxInvalidPasswordAttempts { get; set; } = 5;
        public bool EnablePasswordExpire { get; set; } = false;
        public int PasswordExpireWindow { get; set; } = 60;
        public int PasswordAttemptWindow { get; set; } = 10;
        public int MinRequiredNonAlphanumericCharacters { get; set; } = 1;
        public int MinRequiredPasswordLength { get; set; } = 6;
        public string PasswordStrengthRegularExpression { get; set; } = "";
        public string EmailRegularExpression { get; set; } = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
        public string PhoneRegularExpression { get; set; } = @"\+?[0-9]{10}";
        public bool SendActivationEmailAfterSignup { get; set; } = true;
        public string EmailVerificationUrl { get; set; }
        public string InvitationUrl { get; set; }

        //private static AuthenticationService defaultInstance;
        //public static AuthenticationService Instance
        //{
        //    get
        //    {
        //        if (defaultInstance == null)
        //            defaultInstance = new AuthenticationService();

        //        return defaultInstance;
        //    }
        //}

        private readonly IDataProvider _dataManager;
        private readonly INotificationProvider _notificationProvider;

        public AuthenticationService(IDataProvider dataManager, INotificationProvider notificationProvider = null)
        {
            _dataManager = dataManager;
            _notificationProvider = notificationProvider;
        }

        //public async Task<AuthCode> SendCode(string phone, AuthCodeMessageType messageType)
        //{
        //    if (string.IsNullOrEmpty(phone))
        //        throw new ApplicationException(Resx.AppResources.InvalidPhoneException);

        //    var code = OtpTools.GenRandomNumber(6);
        //    var authCode = new AuthCode()
        //    {
        //        Phone = phone,
        //        IsRegistered = false,
        //        MessageType = AuthCodeMessageType.SmsMessageWithCode,
        //        IsPassword = false,
        //        CodeHash = CryptoProvider.SHA1(CryptoProvider.SHA1(code)).ToLower(),
        //        //Token = CryptoProvider.HMACSHA1(phone, OtpTools.GetOtpTime()).ToLower(),
        //    };
                        
        //    return await Task.Run<AuthCode>(async () =>
        //    {
        //        var user = _dataManager.Get<User>(new { Phone = phone });
        //        if (user != null)
        //        {
        //            authCode.IsRegistered = true;
        //        }

        //        authCode.CreateTime = DateTimeOffset.UtcNow;
        //        authCode.ExpieryTime = DateTimeOffset.UtcNow.AddSeconds(180);
        //        authCode.Id = _dataManager.Insert<AuthCode, long>(authCode);

        //        // Send Message
        //        _notificationProvider?.SendPhoneVerificationMessage(phone, code, user?.AppName, messageType);

        //        return authCode;
        //    });
        //}

        public async Task<AuthCode> SendCode(string recipient, AuthCodeMessageType messageType, string appName)
        {
            if (string.IsNullOrEmpty(recipient))
                throw new ApplicationException("Invalid recipient.");

            User user = null;
            if (messageType == AuthCodeMessageType.Email)
                user = _dataManager.Get<User>(new { Email = recipient });
            else
                user = _dataManager.Get<User>(new { Phone = recipient });

            var code = OtpTools.GenRandomNumber(6);
            var authCode = new AuthCode()
            {
                Recipient = recipient,
                IsRegistered = user != null,
                MessageType = messageType,
                IsPassword = false,
                CodeHash = CryptoProvider.SHA1(CryptoProvider.SHA1(code)).ToLower(),
                //Token = CryptoProvider.HMACSHA1(phone, OtpTools.GetOtpTime()).ToLower(),
                CreateTime = DateTimeOffset.UtcNow,
                ExpieryTime = messageType == AuthCodeMessageType.Email ? DateTimeOffset.UtcNow.AddDays(30) : DateTimeOffset.UtcNow.AddSeconds(180)
            };
            authCode.Id = _dataManager.Insert<AuthCode, long>(authCode);

            // Send Message
            switch (messageType)
            {
                case AuthCodeMessageType.SmsMessageWithCode:
                case AuthCodeMessageType.SmsMessageWithAppLink:
                case AuthCodeMessageType.ChatMessage:
                case AuthCodeMessageType.PhoneCall:
                case AuthCodeMessageType.PushMessage:
                    await _notificationProvider?.SendPhoneVerificationMessage(recipient, user?.DisplayName, code, appName);
                    break;

                case AuthCodeMessageType.Email:
                    var token = Convert.ToBase64String(Encoding.Unicode.GetBytes($"{recipient}&{code}&{authCode.ExpieryTime}"));
                    var link = $"{EmailVerificationUrl}?token={HttpUtility.UrlEncode(token)}";
                    await _notificationProvider?.SendEmailVerificationMessage(recipient, user?.DisplayName, link, appName);
                    break;

                default:
                    break;
            }

            return authCode;
        }

        internal async Task<AuthCode> SendCodeToUser(User user, AuthCodeMessageType messageType, string appName)
        {
            if (user == null)
                throw new UserRightException(Resx.AppResources.UserNotExistsException);

            string recipient = messageType == AuthCodeMessageType.Email ? user.Email : user.Phone;
            var code = OtpTools.GenRandomNumber(6);
            var authCode = new AuthCode()
            {
                Recipient = recipient,
                IsRegistered = user != null,
                MessageType = messageType,
                IsPassword = false,
                CodeHash = CryptoProvider.SHA1(CryptoProvider.SHA1(code)).ToLower(),
                //Token = CryptoProvider.HMACSHA1(phone, OtpTools.GetOtpTime()).ToLower(),
                CreateTime = DateTimeOffset.UtcNow,
                ExpieryTime = messageType == AuthCodeMessageType.Email ? DateTimeOffset.UtcNow.AddDays(30) : DateTimeOffset.UtcNow.AddSeconds(180)
            };
            authCode.Id = _dataManager.Insert<AuthCode, long>(authCode);

            // Send Message
            switch (messageType)
            {
                case AuthCodeMessageType.SmsMessageWithCode:
                case AuthCodeMessageType.SmsMessageWithAppLink:
                case AuthCodeMessageType.ChatMessage:
                case AuthCodeMessageType.PhoneCall:
                case AuthCodeMessageType.PushMessage:
                    await _notificationProvider?.SendPhoneVerificationMessage(recipient, user?.DisplayName, code, appName);
                    break;

                case AuthCodeMessageType.Email:
                    var token = Convert.ToBase64String(Encoding.Unicode.GetBytes($"{recipient}&{code}&{authCode.ExpieryTime}"));
                    var link = $"{EmailVerificationUrl}?token={HttpUtility.UrlEncode(token)}";
                    await _notificationProvider?.SendEmailVerificationMessage(recipient, user?.DisplayName, link, appName);
                    break;

                default:
                    break;
            }

            return authCode;
        }

        public async Task<AuthCode> SendCodeToUser(string username, AuthCodeMessageType messageType, string appName)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new UserRightException(Resx.AppResources.InvalidUsernameException);

            return await Task.Run(async () =>
            {
                var user = _dataManager.Get<User>(new { Name = username });
                return await SendCodeToUser(user, messageType, appName);
            });
        }

        public async Task<AuthCode> CheckCode(string recipient, string code, bool updateUsage = true)
        {
            return await Task.Run<AuthCode>(() =>
            {
                AuthCode authCode = null;
                var codeQuery = new { Recipient = recipient, CodeHash = CryptoProvider.SHA1(code.ToUpper()).ToLower() };
                authCode = _dataManager.Get<AuthCode>(codeQuery);
                if (authCode == null || authCode.UsedTime.HasValue)
                    throw new ApplicationException("کد تائید کاربر نامعتبر میباشد.");

                if (authCode.ExpieryTime < DateTimeOffset.UtcNow)
                    throw new ApplicationException("کد تائید کاربر منقضی شده و غیر قابل استفاده میباشد.");

                if (updateUsage)
                {
                    var usedTime = DateTimeOffset.UtcNow;
                    var count = _dataManager.Update<AuthCode>(new { UsedTime = usedTime }, new { Id = authCode.Id });
                    if (count <= 0)
                        throw new ApplicationException("خطا در بروز رسانی وضعیت کد تائید کاربر.");
                    authCode.UsedTime = usedTime;
                }

                return authCode;
            });
        }
        public async Task<AuthCode> VerifyPhone(string phone, string code, bool approveUser = false)
        {
            var authCode = await CheckCode(phone, code, true);            
            if (approveUser && authCode.IsRegistered)
            {
                _dataManager.Update<User>(new { IsApproved = true }, new { Phone = authCode.Recipient });
            }
            return authCode;
        }
        public async Task<AuthCode> VerifyEmail(string token, bool approveUser = false)
        {
            var value = HttpUtility.UrlDecode(token);
            var items = Encoding.Unicode.GetString(Convert.FromBase64String(value)).Split(new Char[] { '&' });
            string email = items?.Count() > 0 ? items[0] : null;
            var code = items?.Count() > 1 ? items[1] : null;
            var authCode = await CheckCode(email, CryptoProvider.SHA1(code), false);
            if (approveUser && authCode.IsRegistered)
            {
                _dataManager.Update<User>(new { IsApproved = true }, new { Email = authCode.Recipient });
            }
            return authCode;
        }

        public string GenerateInvitationLink(string referee)
        {
            var link = $"{InvitationUrl}?referee=" + referee;

            return link;
        }
        public async Task Invite(string recipient, string referee, AuthCodeMessageType messageType, string appName)
        {
            if (string.IsNullOrEmpty(recipient))
                throw new ApplicationException("Invalid recipient.");

            var exists = false;
            if (messageType == AuthCodeMessageType.Email)
                exists = _dataManager.Exists<User>(new { Email = recipient });
            else
                exists = _dataManager.Exists<User>(new { Phone = recipient });

            if (exists)
                throw new UserRightException("کاربری با این مشخصات از قبل عضو سامانه میباشد.");

            var link = GenerateInvitationLink(referee);

            // Send Message
            switch (messageType)
            {
                case AuthCodeMessageType.SmsMessageWithCode:
                case AuthCodeMessageType.SmsMessageWithAppLink:
                case AuthCodeMessageType.ChatMessage:
                case AuthCodeMessageType.PhoneCall:
                case AuthCodeMessageType.PushMessage:
                    await _notificationProvider?.SendInvitationPhoneMessage(recipient, link, appName);
                    break;

                case AuthCodeMessageType.Email:
                    await _notificationProvider?.SendInvitationEmailMessage(recipient, link, appName);
                    break;

                default:
                    break;
            }
        }
        public async Task<User> Register(string userName, string password, string firstName, string lastName, string phone, string email)
        {
            var authCode = await CheckCode(phone, password);
            // TODO: update code used status ASAP

            return await Task.Run<User>(() =>
            {
                User user = null;
                user = new User()
                {
                    Name = userName,
                    FirstName = firstName,
                    LastName = lastName,
                    Password = password,
                    PasswordFormat = PasswordFormatType.OtpMessage,
                    Phone = phone,
                    Email = email,
                    IsApproved = true,
                    AppName = "ChatBox"
                };

                user.Id = _dataManager.Insert<User, long>(user);
                _dataManager.Update<AuthCode>(new { UsedTime = DateTimeOffset.UtcNow }, new { Id = authCode.Id });

                return user;
            });
        }
        public async Task<User> Register(string username, string password, string passwordConfirm, PasswordFormatType passwordFormat,
            string email, bool isApproved, string appName)
        {
            return await Register(username, password, passwordConfirm, passwordFormat, null, null, null, email, isApproved, true, false, false, appName);
        }

        public async Task<User> Register(string username, string password, string passwordConfirm, PasswordFormatType passwordFormat,
            string firstName, string lastName, string phone, string email, bool isApproved, bool allowChangePassword, bool mustChangePassword, bool isLockedOut, string appName)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new UserRightException(Resx.AppResources.InvalidUsernameException);

            if (password != passwordConfirm)
                throw new ApplicationException(Resx.AppResources.PasswordNotMachException);

            if (string.IsNullOrWhiteSpace(email) || !new Regex(EmailRegularExpression).Match(email).Success)
                throw new UserRightException(Resx.AppResources.InvalidEmailException);

            if (string.IsNullOrWhiteSpace(phone) || !new Regex(PhoneRegularExpression).Match(phone).Success)
                throw new UserRightException(Resx.AppResources.InvalidPhoneException);

            if (string.IsNullOrWhiteSpace(password) || password.Length < MinRequiredPasswordLength)
                throw new UserRightException(string.Format(Resx.AppResources.InvalidPasswordException, MinRequiredPasswordLength, MinRequiredNonAlphanumericCharacters));

            int num = password.Where((t, i) => !char.IsLetterOrDigit(password, i)).Count();
            if (num < MinRequiredNonAlphanumericCharacters)
                throw new UserRightException(string.Format(Resx.AppResources.InvalidPasswordException, MinRequiredPasswordLength, MinRequiredNonAlphanumericCharacters));

            if (!string.IsNullOrWhiteSpace(PasswordStrengthRegularExpression))
            {
                var regex = new Regex(PasswordStrengthRegularExpression);
                if(!regex.Match(password).Success)
                    throw new UserRightException(string.Format(Resx.AppResources.InvalidPasswordException, MinRequiredPasswordLength, MinRequiredNonAlphanumericCharacters));
            }

            if (RequiresUniqueEmail)
            {
                var item = _dataManager.Get<User>(new { Email = email });
                if(item != null)
                    throw new UserRightException(Resx.AppResources.DuplicateEmailException);
            }

            if (RequiresUniquePhone)
            {
                var item = _dataManager.Get<User>(new { Phone = phone });
                if (item != null)
                    throw new UserRightException(Resx.AppResources.DuplicatePhoneException);
            }

            var u = _dataManager.Get<User>(new { Name = username });
            if (u != null)
                throw new UserRightException(Resx.AppResources.DuplicateUserNameException);

            return await Task.Run<User>(() =>
            {
                var user = new User()
                {
                    Name = username,
                    FirstName = firstName,
                    LastName = lastName,
                    Password = EncodePassword(password, passwordFormat, username),
                    PasswordFormat = passwordFormat,
                    Phone = phone,
                    Email = email,
                    IsApproved = isApproved,
                    AllowChangePassword = allowChangePassword,
                    MustChangePassword = mustChangePassword,
                    IsLockedOut = isLockedOut,
                    AppName = appName
                };

                user.Id = _dataManager.Insert<User, long>(user);

                if (SendActivationEmailAfterSignup)
                {
                    SendCodeToUser(user, AuthCodeMessageType.Email, "قبض یار");
                }

                return user;
            });
        }

        public GroupUser AddGroupUser(string groupName, long userId)
        {
            var groupUser = new GroupUser
            {
                GroupName = groupName,
                UserId = userId,
            };
            _dataManager.Insert<GroupUser>(groupUser);

            return groupUser;
        }

        public async Task<bool> UpdateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(user.Name))
                throw new UserRightException(Resx.AppResources.InvalidUsernameException);
            
            if (string.IsNullOrWhiteSpace(user.Email) || !new Regex(EmailRegularExpression).Match(user.Email).Success)
                throw new UserRightException(Resx.AppResources.InvalidEmailException);

            if (string.IsNullOrWhiteSpace(user.Phone) || !new Regex(PhoneRegularExpression).Match(user.Phone).Success)
                throw new UserRightException(Resx.AppResources.InvalidPhoneException);

            if (RequiresUniqueEmail)
            {
                //var exists = _dataManager.Exists<User>(new { Email = user.Email, id != id});
                //if (exists)
                //    throw new UserRightException(Resx.AppResources.DuplicateEmailException);

                //var q = from u in _dataManager.Users where u.Email == user.Email && u.Id != user.Id select new { u.Id };
                //var count = q.Count();//invalid table name error
                var sql = @"select count(*) from users where Email = @Email and Id != @Id";
                var count = (decimal)_dataManager.ExecuteScalar(sql, new { user.Email, user.Id });
                if (count > 0)
                    throw new UserRightException(Resx.AppResources.DuplicateEmailException);
            }

            if (RequiresUniquePhone)
            {
                //var exists = _dataManager.Exists<User>(new { Phone = user.Phone, id != id });
                //if (exists)
                //    throw new UserRightException(Resx.AppResources.DuplicatePhoneException);

                var sql = @"select count(*) from users where Phone = @Phone and Id != @Id";
                var count = (decimal)_dataManager.ExecuteScalar(sql, new { user.Phone, user.Id });
                if (count > 0)
                    throw new UserRightException(Resx.AppResources.DuplicatePhoneException);
            }

            var sqlu = @"select count(*) from users where Name = @Name and Id != @Id";
            var countu = (decimal)_dataManager.ExecuteScalar(sqlu, new { Name = user.Name, user.Id });
            if (countu > 0)
                throw new UserRightException(Resx.AppResources.DuplicateUserNameException);

            return await Task.Run<bool>(() =>
            {
                return _dataManager.Update<User>(new
                {
                    user.Name,
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    user.Phone,
                    user.IsApproved,
                    user.IsLockedOut,
                    user.AllowChangePassword,
                    user.MustChangePassword,
                    user.RemoteClients,
                    user.Description,
                }, new { user.Id }) == 1;
            });
        }

        public User Login(string username, string password)
        {
            return Login(username, password, true, true);
        }
        public User Login(string username, string password, bool updateLastLoginInfo, bool updateFailureCount)
        {
            //using (var dataManager = new DataManager())
            {
                var user = _dataManager.Get<User>(new { Name = username });
                if (user == null)
                    throw new UserRightException(Resx.AppResources.UserNotExistsException);

                if (!user.IsApproved)
                    throw new UserRightException(Resx.AppResources.UserNotApprovedException);

                if (user.IsLockedOut)
                    throw new UserRightException(Resx.AppResources.UserIsLockedOutException);

                if (user.MustChangePassword)
                    throw new ChangePasswordNextLoginException();

                if (EnablePasswordExpire && user.IsPasswordExpired)
                    throw new PasswordExpireException();
                
                if (ValidatePassword(user, password))
                {
                    user.IsOnline = true;
                    user.LastLoginTime = DateTimeOffset.UtcNow;
                    user.LastActivityTime = DateTimeOffset.UtcNow;

                    if (updateLastLoginInfo)
                    {
                        if (!_dataManager.UpdateUserLoginStatus(user))// this.Update())
                            throw new UserRightException("Unable to update user state.");
                    }
                }
                else
                {
                    if (updateFailureCount)
                    {
                        ValidatePasswordFailureCount(ref user);
                        _dataManager.UpdateUserPasswordFailureCount(user);
                    }
                    throw new InvalidUserNameOrPasswordException();
                }

                return user;
            }
        }        
        public bool Logout(string username)
        {
            //var user = _dataManager.Get<User>(new { Name = username });
            //if (user == null)
            //    throw new UserRightException(Resx.AppResources.UserNotExistsException);

            var count = _dataManager.Update<User>(new { IsOnline = false }, new { Name = username });
                                   
            return count == 1;
        }        
        public bool ChangePassword(string username, string oldPwd, string newPwd, bool updateFailureCount)
        {
            bool IsChangePsw = false;

            //using (var dataManager = new DataManager())
            {
                var user = _dataManager.Get<User>(new { Name = username });
                if (user == null)
                    throw new UserRightException(Resx.AppResources.UserNotExistsException);

                if (!user.IsApproved)
                    throw new UserRightException(Resx.AppResources.UserNotApprovedException);

                if (user.IsLockedOut)
                    throw new UserRightException(Resx.AppResources.UserIsLockedOutException);

                if (!user.AllowChangePassword)
                    throw new AccessDeniedException(new ChangePasswordRight());

                if (!ValidatePassword(user, oldPwd))
                {
                    if (updateFailureCount)
                    {
                        ValidatePasswordFailureCount(ref user);
                        _dataManager.UpdateUserPasswordFailureCount(user);
                    }
                    throw new InvalidUserNameOrPasswordException();
                }

                user.Password = EncodePassword(newPwd, user.PasswordFormat, user.Name);
                user.LastPasswordChangeTime = DateTimeOffset.UtcNow;
                user.MustChangePassword = false;
                user.PasswordExpireTime = EnablePasswordExpire ? DateTimeOffset.UtcNow.AddDays(PasswordExpireWindow) : (DateTimeOffset?)null;

                if (!_dataManager.UpdateUserPasswordStatus(user))
                    throw new UserRightException("Unable to update user new password.");

                IsChangePsw = true;

            }

            return IsChangePsw;
        }
        public bool ChangePasswordQuestionAndAnswer(string username, string password,
                      string newPwdQuestion,
                      string newPwdAnswer,
                      bool updateFailureCount)
        {
            bool IsChangePsw = false;

            //using (var dataManager = new DataManager())
            {
                var user = _dataManager.Get<User>(new { Name = username });
                if (user == null)
                    throw new UserRightException(Resx.AppResources.UserNotExistsException);

                if (!user.IsApproved)
                    throw new UserRightException(Resx.AppResources.UserNotApprovedException);

                if (user.IsLockedOut)
                    throw new UserRightException(Resx.AppResources.UserIsLockedOutException);

                if (!ValidatePassword(user, password))
                {
                    if (updateFailureCount)
                    {
                        ValidatePasswordFailureCount(ref user);
                        _dataManager.UpdateUserPasswordFailureCount(user);
                    }
                    throw new InvalidUserNameOrPasswordException();
                }

                user.PasswordQuestion = newPwdQuestion;
                user.PasswordAnswer = EncodePassword(newPwdAnswer, user.PasswordFormat, user.Name);

                if (!_dataManager.UpdateUserQuestionAndAnswer(user))
                    throw new UserRightException("Unable to update user new password question and answer.");

                IsChangePsw = true;
            }
            return IsChangePsw;
        }
        public string ResetPassword(string username, string answer)
        {
            if (!EnablePasswordReset)
                throw new NotSupportedException("Password reset is not enabled.");

            if (answer == null && RequiresQuestionAndAnswer)
                throw new UserRightException("Password answer required for password reset.");

            //using (var dataManager = new DataManager())
            {
                var user = _dataManager.Get<User>(new { Name = username });
                if (user == null)
                    throw new UserRightException(Resx.AppResources.UserNotExistsException);

                if (!user.IsApproved)
                    throw new UserRightException(Resx.AppResources.UserNotApprovedException);

                if (user.IsLockedOut)
                    throw new UserRightException(Resx.AppResources.UserIsLockedOutException);

                if (RequiresQuestionAndAnswer && !ValidatePasswordAnswer(user, answer))
                {
                    ValidatePasswordAnswerFailureCount(ref user);
                    _dataManager.UpdateUserPasswordAnswerFailureCount(user);
                    throw new UserRightException("Incorrect password answer.");
                }

#if NET45
                string newPassword =
                System.Web.Security.Membership.GeneratePassword(NewPasswordLength, MinRequiredNonAlphanumericCharacters);
#else
                string newPassword =
                PasswordGenerator.GeneratePassword(NewPasswordLength, MinRequiredNonAlphanumericCharacters);
#endif
                user.Password = EncodePassword(newPassword, user.PasswordFormat, user.Name);
                user.LastPasswordChangeTime = DateTimeOffset.UtcNow;
                user.MustChangePassword = true;
                user.PasswordExpireTime = EnablePasswordExpire ? DateTimeOffset.UtcNow.AddDays(PasswordExpireWindow) : (DateTimeOffset?)null;

                if (!_dataManager.UpdateUserPasswordStatus(user))
                    throw new UserRightException("Unable to update user new password.");

                return newPassword;
            }
            
        }

        public string GetPassword(string username, string answer)
        {
            if (!EnablePasswordRetrieval)
                throw new UserRightException("Password Retrieval Not Enabled.");

            //using (var dataManager = new DataManager())
            {
                var user = _dataManager.Get<User>(new { Name = username });
                if (user == null)
                    throw new UserRightException(Resx.AppResources.UserNotExistsException);

                if (user.IsLockedOut)
                    throw new UserRightException(Resx.AppResources.UserIsLockedOutException);

                if (RequiresQuestionAndAnswer && !ValidatePasswordAnswer(user, answer))
                {
                    ValidatePasswordAnswerFailureCount(ref user);
                    _dataManager.UpdateUserPasswordAnswerFailureCount(user);
                    throw new UserRightException("Incorrect password answer.");
                }

                return DecodePassword(user.Password, user.PasswordFormat, user.Name);
            }
        }

        public bool UnlockUser(string username)
        {
            //using (var dataManager = new DataManager())
            {
                var user = _dataManager.Get<User>(new { Name = username });
                if (user == null)
                    throw new UserRightException(Resx.AppResources.UserNotExistsException);

                user.IsLockedOut = false;
                user.FailedPswAttemptCount = 0;

                if (!_dataManager.UpdateUserUnlock(user))
                    throw new UserRightException("Unable to update user.");
            }
            return true;
        }

        private void ValidatePasswordFailureCount(ref User user)
        {
            int failureCount = user.FailedPswAttemptCount;
            DateTimeOffset windowStart = user.FailedPswAttemptWinStart;
            DateTimeOffset windowEnd = windowStart.AddMinutes(PasswordAttemptWindow);

            if (failureCount == 0 || DateTimeOffset.UtcNow > windowEnd)
            {
                // First password failure or outside of PasswordAttemptWindow. 
                // Start a new password failure count from 1 and a new window starting now.

                user.FailedPswAttemptCount = 1;
                user.FailedPswAttemptWinStart = DateTimeOffset.UtcNow;
            }
            else
            {
                if (failureCount++ > MaxInvalidPasswordAttempts)
                {
                    // Password attempts have exceeded the failure threshold. Lock out
                    // the user.

                    user.IsLockedOut = true;
                    user.LastLockedOutTime = DateTimeOffset.UtcNow;
                }
                else
                {
                    // Password attempts have not exceeded the failure threshold. Update
                    // the failure counts. Leave the window the same.

                    user.FailedPswAttemptCount = failureCount;

                }
            }
        }

        private void ValidatePasswordAnswerFailureCount(ref User user)
        {
            int failureCount = user.FailedPswAnswerAttemptCount;
            DateTimeOffset windowStart = user.FailedPswAnswerAttemptWinStart;
            DateTimeOffset windowEnd = windowStart.AddMinutes(PasswordAttemptWindow);

            if (failureCount == 0 || DateTimeOffset.UtcNow > windowEnd)
            {
                // First password failure or outside of PasswordAttemptWindow. 
                // Start a new password failure count from 1 and a new window starting now.

                user.FailedPswAnswerAttemptCount = 1;
                user.FailedPswAnswerAttemptWinStart = DateTimeOffset.UtcNow;
            }
            else
            {
                if (failureCount++ >= MaxInvalidPasswordAttempts)
                {
                    // Password attempts have exceeded the failure threshold. Lock out
                    // the user.

                    user.IsLockedOut = true;
                    user.LastLockedOutTime = DateTimeOffset.UtcNow;
                }
                else
                {
                    // Password attempts have not exceeded the failure threshold. Update
                    // the failure counts. Leave the window the same.

                        user.FailedPswAnswerAttemptCount = failureCount;
                }
            }

        }

        private bool ValidatePassword(User user, string password)
        {
            if (user.PasswordFormat == PasswordFormatType.Otp)
            {
                var app = _dataManager.Get<Application>(new { Name = user.AppName });
                if (app == null)
                    throw new ApplicationNotFoundException();

                return GenerateOtp(app, user, OtpTools.GetOtpTime()) == password;
            }
            else if (user.PasswordFormat == PasswordFormatType.OtpMessage)
            {
                CheckCode(user.Phone, password, true).GetAwaiter().GetResult();
                
                return true;
            }
            else
                return EncodePassword(password, user.PasswordFormat, user.Name) == user.Password;
        }

        private bool ValidatePasswordAnswer(User user, string answer)
        {
            return EncodePassword(answer, user.PasswordFormat, user.Name) == user.PasswordAnswer;
        }

        private string EncodePassword(string password, PasswordFormatType passwordFormat, string key)
        {
            string encodedPassword = password;

            if (password != null) // PasswordAnswer some times sends null to encode
            {
                switch (passwordFormat)
                {
                    case PasswordFormatType.Clear:
                        break;
                    case PasswordFormatType.Hashed:
                        HMACSHA1 hash = new HMACSHA1();
                        //hash.Key = HexToByte(machineKey.ValidationKey);
                        hash.Key = Encoding.UTF8.GetBytes(key);
                        //encodedPassword =
                        //  Convert.ToBase64String(hash.ComputeHash(Encoding.UTF8.GetBytes(password)));

                        encodedPassword = BitConverter.ToString(hash.ComputeHash(Encoding.UTF8.GetBytes(password))).Replace("-", string.Empty);
                        //System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile()
                        break;
                    case PasswordFormatType.Encrypted:
                        encodedPassword =
                          Convert.ToBase64String(EncryptPassword(Encoding.UTF8.GetBytes(password)));
                        break;
                    case PasswordFormatType.Otp:
                        break;

                    case PasswordFormatType.OtpMessage:
                        break;
                    default:
                        throw new UserRightException("Unsupported password format.");
                }
            }
            return encodedPassword;
        }

        private string DecodePassword(string password, PasswordFormatType passwordFormat, string key)
        {
            string pass = password;

            switch (passwordFormat)
            {
                case PasswordFormatType.Clear:
                    break;
                case PasswordFormatType.Encrypted:
                    pass =
                      Encoding.Unicode.GetString(DecryptPassword(Convert.FromBase64String(password)));
                    break;
                case PasswordFormatType.Hashed:
                    throw new UserRightException("Cannot unencode a hashed password.");
                default:
                    throw new UserRightException("Unsupported password format.");
            }

            return pass;
        }

        private byte[] EncryptPassword(byte[] password)
        {
            return new byte[8];
        }
        private byte[] DecryptPassword(byte[] password)
        {
            return new byte[8];
        }


        public bool ValidateOtp(string username, string challenge, string otp)
        {
            //using (var dataManager = new DataManager())
            {
                var user = _dataManager.Get<User>(new { Name = username });
                if (user == null)
                    throw new UserRightException(Resx.AppResources.UserNotExistsException);

                var app = _dataManager.Get<Application>(new { Name = user.AppName });
                if (app == null)
                    throw new ApplicationNotFoundException();

                return GenerateOtp(app, user, challenge) == otp;
            }
        }
        public bool ValidateSigne(string username, string data, string signe)
        {
            //using (var dataManager = new DataManager())
            {
                var user = _dataManager.Get<User>(new { Name = username });
                if (user == null)
                    throw new UserRightException(Resx.AppResources.UserNotExistsException);

                var app = _dataManager.Get<Application>(new { Name = user.AppName });
                if (app == null)
                    throw new ApplicationNotFoundException();

                return Signe(app, user, data) == signe;
            }
        }
        public bool ValidateOtpByTime(string username, string otp)
        {
            return ValidateOtp(username, OtpTools.GetOtpTime(), otp);
        }
        public bool ValidateOtpByCounter(string username, string otp)
        {
            //using (var dataManager = new DataManager())
            {
                var user = _dataManager.Get<User>(new { Name = username });
                if (user == null)
                    throw new UserRightException(Resx.AppResources.UserNotExistsException);

                var app = _dataManager.Get<Application>(new { Name = user.AppName });
                if (app == null)
                    throw new ApplicationNotFoundException();

                return GenerateOtp(app, user, user.Counter.ToString()) == otp;
            }
        }
        private string GenerateOtp(Application app, User user, string challenge)
        {
            byte[] otp = OtpTools.GenerateOtp(app.AppKey, user.UserKey, challenge, (OtpAlgorithmEnum)app.OtpAuthAlgorithm);
            string ret = "";
            switch ((OtpValueTypeEnum)app.OtpAuthValueType)
            {
                case OtpValueTypeEnum.Raw:
                    ret = Encoding.ASCII.GetString(otp);
                    break;
                case OtpValueTypeEnum.HexaDecimal:
                    ret = OtpTools.ByteArrayToHexString(otp);
                    break;
                case OtpValueTypeEnum.Numerical:
                    ret = OtpTools.ByteArrayToDecimalString(otp);
                    break;
            }
            if (app.OtpAuthValueLength > 0 && app.OtpAuthValueLength < ret.Length)
                ret = ret.Substring(0, app.OtpAuthValueLength);
            return ret;
        }
        private string Signe(Application app, User user, string data)
        {
            byte[] otp = OtpTools.GenerateOtp(app.AppKey, user.UserKey, data, (OtpAlgorithmEnum)app.OtpSigneAlgorithm);
            string ret = "";
            switch ((OtpValueTypeEnum)app.OtpSigneValueType)
            {
                case OtpValueTypeEnum.Raw:
                    ret = Encoding.ASCII.GetString(otp);
                    break;
                case OtpValueTypeEnum.HexaDecimal:
                    ret = OtpTools.ByteArrayToHexString(otp);
                    break;
                case OtpValueTypeEnum.Numerical:
                    ret = OtpTools.ByteArrayToDecimalString(otp);
                    break;
            }

            if (app.OtpSigneValueLength > 0 && app.OtpSigneValueLength < ret.Length)
                ret = ret.Substring(0, app.OtpSigneValueLength);
            return ret;
        }

    }
}
