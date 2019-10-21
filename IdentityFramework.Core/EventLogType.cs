using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace IdentityFramework.Core
{
    public enum EventLogType
    {
        /// <summary>
        ///  An error event. This indicates a significant problem the user should know about; usually a loss of functionality or data.  
        /// </summary>
        [Description("خطا")]
        Error = 0,

        /// <summary>
        ///  A warning event. This indicates a problem that is not immediately significant, but that may signify conditions that could cause future problems.  
        /// </summary>
        [Description("اخطار")]
        Warning = 1,

        /// <summary>
        ///  An information event. This indicates a significant, successful operation.  
        /// </summary>
        [Description("اطلاعات")]
        Information = 2,

        /// <summary>
        /// A success audit event. This indicates a security event that occurs when an audited access attempt is successful; for example, logging on successfully.  
        /// </summary>
        [Description("موفق")]
        SuccessAudit = 3,

        /// <summary>
        /// A failure audit event. This indicates a security event that occurs when an audited access attempt fails; for example, a failed attempt to open a file.  
        /// </summary>
        [Description("ناموفق")]
        FailureAudit = 4,
    }
}
