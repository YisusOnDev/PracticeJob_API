using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PracticeJob.Core.Email
{
    public class EmailTemplates
    {
        public static string ConfirmEmailTemplate = Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.EndsWith("ConfirmEmailTemplate.html"));
        public static string PasswordRecoveryTemplate = Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.EndsWith("ResetPasswordTemplate.html"));
        public static string CompanyContactTemplate = Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.EndsWith("CompanyContactTemplate.html"));
    }
}
