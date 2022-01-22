using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeJob.Core.Email
{
    public class EmailTemplates
    {
        private static readonly string TemplatesPath = @"../PracticeJob.Core/Email/EmailTemplates/";
        public static string ConfirmEmailTemplate = TemplatesPath + "ConfirmEmailTemplate.html";
        public static string PasswordRecoveryTemplate = TemplatesPath + "ResetPasswordTemplate.html";
    }
}
