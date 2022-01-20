using Microsoft.Extensions.Configuration;
using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PracticeJob.Core.Email
{
    public class EmailSender : IEmailSender
    {
        private IConfiguration IConfiguration;
        public EmailSender(IConfiguration IConfiguration)
        {
            this.IConfiguration = IConfiguration;
        }
        public void SendConfirmationMail(string destinationEmail, string name, string confirmationCode)
        {
            string emailTemplate = File.ReadAllText(@"../PracticeJob.Core/Email/EmailTemplates/ConfirmEmailTemplate.html");
            string mailBody = Engine.Razor.RunCompile(emailTemplate, "confirmMail", null, new { Name = name, Code = confirmationCode });
            
            MailAddress fromAddress = new MailAddress(IConfiguration["EmailSettings:Email"], "PracticeJob");
            string fromPassword = IConfiguration["EmailSettings:Password"];

            MailAddress toAddress = new MailAddress(destinationEmail);

            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            
            MailMessage mail = new MailMessage(fromAddress, toAddress);

            mail.IsBodyHtml = true;
            mail.Subject = "Confirma tu nueva cuenta en PracticeJob";
            mail.Body = mailBody;

            smtp.SendAsync(mail, destinationEmail);
        }

        public void SendPasswordReset(string destinationEmail, string name, string confirmationCode)
        {
            string emailTemplate = File.ReadAllText(@"../PracticeJob.Core/Email/EmailTemplates/ResetPasswordTemplate.html");
            string mailBody = Engine.Razor.RunCompile(emailTemplate, "resetPassword", null, new { Name = name, Code = confirmationCode });

            MailAddress fromAddress = new MailAddress(IConfiguration["EmailSettings:Email"], "PracticeJob");
            string fromPassword = IConfiguration["EmailSettings:Password"];

            MailAddress toAddress = new MailAddress(destinationEmail);

            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            MailMessage mail = new MailMessage(fromAddress, toAddress);

            mail.IsBodyHtml = true;
            mail.Subject = "Confirma tu nueva cuenta en PracticeJob";
            mail.Body = mailBody;

            smtp.SendAsync(mail, destinationEmail);
        }
    }
}
