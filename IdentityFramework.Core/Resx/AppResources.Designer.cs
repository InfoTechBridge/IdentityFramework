﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IdentityFramework.Core.Resx {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class AppResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AppResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("IdentityFramework.Core.Resx.AppResources", typeof(AppResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to شما اجازه {0} را ندارید..
        /// </summary>
        public static string AccessDeniedException {
            get {
                return ResourceManager.GetString("AccessDeniedException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to مدیریت سیستم.
        /// </summary>
        public static string AdministrationRight {
            get {
                return ResourceManager.GetString("AdministrationRight", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to برنامه مورد نظر موجود نميباشد..
        /// </summary>
        public static string ApplicationNotFoundException {
            get {
                return ResourceManager.GetString("ApplicationNotFoundException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to كلمه عبور ميبايست در اولين ورود تغيير يابد..
        /// </summary>
        public static string ChangePasswordNextLoginException {
            get {
                return ResourceManager.GetString("ChangePasswordNextLoginException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to تغییر کلمه عبور.
        /// </summary>
        public static string ChangePasswordRight {
            get {
                return ResourceManager.GetString("ChangePasswordRight", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to آدرس ایمیل وارد شده تکراری میباشد..
        /// </summary>
        public static string DuplicateEmailException {
            get {
                return ResourceManager.GetString("DuplicateEmailException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to شماره تلفن وارد شده تکراری میباشد..
        /// </summary>
        public static string DuplicatePhoneException {
            get {
                return ResourceManager.GetString("DuplicatePhoneException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to نام کاربری وارد شده تکراری میباشد..
        /// </summary>
        public static string DuplicateUserNameException {
            get {
                return ResourceManager.GetString("DuplicateUserNameException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to کاربر گرامی
        ///
        ///لطفا جهت تعریف حساب کاربری، بر روی لینک زیر کلیک نمائید و یا آن را کپی و سپس در قسمت آدرس مرورگر خود وارد نمائید.
        ///
        ///{0}
        ///
        ///با تشکر
        ///{1}.
        /// </summary>
        public static string EmailInvitationBody {
            get {
                return ResourceManager.GetString("EmailInvitationBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to تعریف حساب کاربری {0}.
        /// </summary>
        public static string EmailInvitationSubject {
            get {
                return ResourceManager.GetString("EmailInvitationSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ایمیل نامعتبر میباشد..
        /// </summary>
        public static string InvalidEmailException {
            get {
                return ResourceManager.GetString("InvalidEmailException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to کلمه عبور نامعتبر میباشد. حداقل طول کلمه عبور میبایست {0} کاراکتر و شامل حداقل {1} کاراکتر غیر حرفی عددی باشد..
        /// </summary>
        public static string InvalidPasswordException {
            get {
                return ResourceManager.GetString("InvalidPasswordException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to شماره تلفن نامعتبر میباشد..
        /// </summary>
        public static string InvalidPhoneException {
            get {
                return ResourceManager.GetString("InvalidPhoneException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to نام کاربری نامعتبر میباشد..
        /// </summary>
        public static string InvalidUsernameException {
            get {
                return ResourceManager.GetString("InvalidUsernameException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to نام كاربري و یا كلمه عبور وارد شده نامعتبر ميباشد..
        /// </summary>
        public static string InvalidUserNameOrPasswordExceptin {
            get {
                return ResourceManager.GetString("InvalidUserNameOrPasswordExceptin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ورود به سیستم.
        /// </summary>
        public static string LoginRight {
            get {
                return ResourceManager.GetString("LoginRight", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to کلمه عبور منقضی شده است..
        /// </summary>
        public static string PasswordExpireException {
            get {
                return ResourceManager.GetString("PasswordExpireException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to کلمه عبور با تکرارش یکسان نمیباشد..
        /// </summary>
        public static string PasswordNotMachException {
            get {
                return ResourceManager.GetString("PasswordNotMachException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Your {0} invitation link is {1}..
        /// </summary>
        public static string PhoneInvitationSmsBody {
            get {
                return ResourceManager.GetString("PhoneInvitationSmsBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Your {0} authentication code is {1}..
        /// </summary>
        public static string PhoneVerificationSmsBody {
            get {
                return ResourceManager.GetString("PhoneVerificationSmsBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to تعریف کاربر.
        /// </summary>
        public static string RegisterNewUserRight {
            get {
                return ResourceManager.GetString("RegisterNewUserRight", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to کاربر گرامی {0}
        ///
        ///لطفا جهت فعال سازی حساب کاربری خود بر روی لینک زیر کلیک نمائید و یا آن را کپی و سپس در قسمت آدرس مرورگر خود وارد نمائید.
        ///
        ///{1}
        ///
        ///با تشکر
        ///{2}.
        /// </summary>
        public static string UserActivationEmailBody {
            get {
                return ResourceManager.GetString("UserActivationEmailBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to فعال سازی حساب کاربری {0}.
        /// </summary>
        public static string UserActivationEmailSubject {
            get {
                return ResourceManager.GetString("UserActivationEmailSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to كاربر مورد نظر غير فعال ميباشد..
        /// </summary>
        public static string UserIsLockedOutException {
            get {
                return ResourceManager.GetString("UserIsLockedOutException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to مهم.
        /// </summary>
        public static string UserMessagePriorityHigh {
            get {
                return ResourceManager.GetString("UserMessagePriorityHigh", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to خیلی مهم.
        /// </summary>
        public static string UserMessagePriorityHighest {
            get {
                return ResourceManager.GetString("UserMessagePriorityHighest", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to کم.
        /// </summary>
        public static string UserMessagePriorityLow {
            get {
                return ResourceManager.GetString("UserMessagePriorityLow", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to خیلی کم.
        /// </summary>
        public static string UserMessagePriorityLowest {
            get {
                return ResourceManager.GetString("UserMessagePriorityLowest", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to معمولی.
        /// </summary>
        public static string UserMessagePriorityMedium {
            get {
                return ResourceManager.GetString("UserMessagePriorityMedium", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to كاربر مورد نظر تائید نشده اند..
        /// </summary>
        public static string UserNotApprovedException {
            get {
                return ResourceManager.GetString("UserNotApprovedException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to كاربر مورد نظر موجود نميباشد..
        /// </summary>
        public static string UserNotExistsException {
            get {
                return ResourceManager.GetString("UserNotExistsException", resourceCulture);
            }
        }
    }
}