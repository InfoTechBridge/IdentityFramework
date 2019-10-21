using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace IdentityFramework.Core
{
    
    public enum RightNameEnum
    {
        #region Accounting Rights
        [Category("Accounting")]
        [Description("ورود به سیستم")]
        Logon,

        [Category("Accounting")]
        [Description("تعریف کاربر")]
        RegisterNewUser,

        [Category("Accounting")]
        [Description("ویزایش اطلاعات کاربر")]
        EditUser,

        [Category("Accounting")]
        [Description("تغییر کلمه عبور")]
        ChangePassword,

        [Category("Accounting")]
        [Description("درج کلمه عبور جدید برای خود")]
        ResetSelfPassword,

        [Category("Accounting")]
        [Description("درج کلمه عبور جدید")]
        ResetUsersPassword,

        [Category("Accounting")]
        [Description("مدیریت گروه ها")]
        ManageGroups,

        [Category("Accounting")]
        [Description("مدیریت کاربران")]
        ManageUsers,

        [Category("Accounting")]
        [Description("مدیریت اطلاعات کاربری خود")]
        ManageSelf,

        [Category("Accounting")]
        [Description("مدیریت دسترسی کاربران")]
        ManageUsersRights,

        [Category("Accounting")]
        [Description("مدیریت دسترسی خود")]
        ManageSelfRights,

        [Category("Accounting")]
        [Description("مدیریت گروه های کاربری خود")]
        ManageSelfGroups,

        [Category("Accounting")]
        [Description("مدیریت گروه های کاربری")]
        ManageGroupsUsers,

        [Category("Accounting")]
        [Description("مدیریت دسترسی گروه ها")]
        ManageGroupsRights,

        [Category("Accounting")]
        [Description("مشاهده لیست رخدادها")]
        ViewEventlogs,

        [Category("Accounting")]
        [Description("مدیریت دفترچه تلفن")]
        ManagePhonebook,

        [Category("Accounting")]
        [Description("مدیریت محدودیت ها")]
        ManageAccountLimits,

        [Category("Accounting")]
        [Description("مدیریت محدودیت های خود")]
        ManageSelfAccountLimits,

        [Category("Accounting")]
        [Description("مدیریت کسورات و اضافات")]
        ManageExtraCostTariffs,

        [Category("Accounting")]
        [Description("مدیریت پیام های کاربری")]
        ManageUserMessages,

        #endregion

        #region Financial Rights
        [Category("Financial")]
        [Description("خرید الکترونیکی")]
        DoPayment,

        [Category("Financial")]
        [Description("ثبت سفارش خرید اعتبار")]
        RegPurchaseOrder,

        [Category("Financial")]
        [Description("ثبت فیش پرداخت")]
        RegPaymentInfo,

        [Category("Financial")]
        [Description("مشاهده ریز سفارش های خرید")]
        ViewPurchaseOrders,

        [Category("Financial")]
        [Description("تائید سفارش خرید")]
        AcceptPurchaseOrder,

        [Category("Financial")]
        [Description("مشاهده ریز پرداخت ها")]
        ViewPayments,

        [Category("Financial")]
        [Description("تائید پرداخت")]
        VerifyPaymentManualy,

        [Category("Financial")]
        [Description("تائید نهائی پرداخت")]
        AcceptPayment,

        [Category("Financial")]
        [Description("تائید اصلاحیه مالی")]
        AcceptAdjustment,

        [Category("Financial")]
        [Description("چاپ فاکتور")]
        PrintInvoice,

        [Category("Financial")]
        [Description("مشاهده لیست فاکتورهای صادر شده")]
        ViewPrintInvoices,

        [Category("Financial")]
        [Description("انتقال اعتبار به سایرین")]
        BalanceTransfer,

        [Category("Financial")]
        [Description("مشاهده آمار پرداخت ها")]
        ViewPaymentStatistics,

        [Category("Financial")]
        [Description("مشاهده برترین های پرداخت")]
        ViewPaymentTops,

        [Category("Financial")]
        [Description("مشاهده ریز اصلاحات مالی")]
        ViewAdjustments,

        [Category("Financial")]
        [Description("ثبت اصلاحیه مالی")]
        AdjustAccount,

        [Category("Financial")]
        [Description("مشاهده ریز انتقال اعتبار")]
        ViewBalanceTransfers,

        [Category("Financial")]
        [Description("مشاهده آمار انتقال اعتبار")]
        ViewBalanceTransStatistics,

        [Category("Financial")]
        [Description("مشاهده صورت وضعیت حساب")]
        ViewAccountSummary,

        [Category("Financial")]
        [Description("مشاهده عملکرد کاربران")]
        ViewAccountStatistics,

        [Category("Financial")]
        [Description("خرید امکانات")]
        BuyModule,

        [Category("Financial")]
        [Description("مشاهده لیست ماژول های خریداری شده")]
        ViewModuleSalesList,

        [Category("Financial")]
        [Description("مشاهده آمار ماژول های خریداری شده")]
        ViewModuleSaleStatistics,

        [Category("Financial")]
        [Description("پرداخت قبض")]
        PayInvoice,

        [Category("Financial")]
        [Description("مدیریت کانال های پرداخت")]
        ManagePaymentChanels,

        #endregion

        #region Topup Rights
        [Category("Topup")]
        [Description("شارژ تلفن اعتباری")]
        DoTopup,

        [Category("Topup")]
        [Description("شارژ تلفن اعتباری در یک مرحله")]
        DoTopupInOneStep,

        [Category("Topup")]
        [Description("مشاهده ریز شارژ تلفن اعتباری")]
        ViewTopups,

        [Category("Topup")]
        [Description("مشاهده آمار شارژ تلفن اعتباری")]
        ViewTopupStatistics,

        [Category("Topup")]
        [Description("مدیریت کانال های شارژ")]
        ManageTopupChanels,

        #endregion

        #region CMS Rights
        [Category("CMS")]
        [Description("مدیریت سایت")]
        ManageSite,

        [Category("CMS")]
        [Description("افزودن صفحه جدید")]
        AddNewContentPage,

        [Category("CMS")]
        [Description("مدیریت صفحات")]
        ManageContentPages,

        [Category("CMS")]
        [Description("مدیریت محتوا")]
        ManageContents,

        [Category("CMS")]
        [Description("افزودن محتوا")]
        AddContent,

        [Category("CMS")]
        [Description("مدیریت اخبار")]
        ManageNews,

        [Category("CMS")]
        [Description("افزودن خبر")]
        AddNews,

        [Category("CMS")]
        [Description("مدیریت فایل ها")]
        ManageFiles,

        #endregion

        #region SMS Rights
        [Category("SMS")]
        [Description("ارسال پیامک تکی")]
        SendMessage,

        [Category("SMS")]
        [Description("ارسال پیامک گروهی")]
        SendGroupMessage,

        [Category("SMS")]
        [Description("ارسال پیامک منطقه ای")]
        SendRegionalMessage,

        [Category("SMS")]
        [Description("ارسال پیامک نظیر به نظیر")]
        SendPeerToPeerMessage,

        [Category("SMS")]
        [Description("مدیریت تعرفه ها")]
        ManageTariffs,

        [Category("SMS")]
        [Description("مدیریت تعرفه های شخصی")]
        ManageSelfTariffs,

        [Category("SMS")]
        [Description("پاسخگوئی خودکار")]
        ReplayToSender,

        [Category("SMS")]
        [Description("انتقال پیام ورودی به موبایل")]
        ForwardToMobile,

        [Category("SMS")]
        [Description("انتقال پیام ورودی به ایمیل")]
        ForwardToEmail,

        [Category("SMS")]
        [Description("ثبت در لیست مخاطبین")]
        SaveToContacts,

        [Category("SMS")]
        [Description("سرویس اوتلوک")]
        OutLookService,

        [Category("SMS")]
        [Description("مشاهده پیام های ارسالی")]
        ViewSendRequests,

        [Category("SMS")]
        [Description("مشاهده پیام های دریافتی")]
        ViewRecieveMessages,

        [Category("SMS")]
        [Description("مشاهده ریز سود ارسال ها")]
        ViewSendBenefits,

        [Category("SMS")]
        [Description("خدمات ارزش افزوده")]
        DoService,

        [Category("SMS")]
        [Description("تعریف خط")]
        DefineLine,

        [Category("SMS")]
        [Description("فروش خط")]
        SaleLine,

        [Category("SMS")]
        [Description("خرید خط")]
        BuyLine,

        [Category("SMS")]
        [Description("مدیریت دسترسی به خطوط")]
        ManageLineAccess,

        [Category("SMS")]
        [Description("مشاهده ریز خرید خط")]
        ViewLineTransactions,

        [Category("SMS")]
        [Description("مشاهده آمار خرید خط")]
        ViewLineSaleStatistics,

        [Category("SMS")]
        [Description("مدیریت خطوط")]
        ManageLines,

        [Category("SMS")]
        [Description("مشاهده آمار ارسال پیامک")]
        ViewSendRequestStatistics,

        [Category("SMS")]
        [Description("مدیریت پیامک های پر کاربرد")]
        ManageMessageTemplates,

        [Category("SMS")]
        [Description("مدیریت خطوط تلگرام")]
        ManageTelegramPhones,

        #endregion
    }
}
