using Microsoft.Extensions.Configuration;
using PracticeJob.Core.DTO;
using PracticeJob.DAL.Entities;
using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
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
        public Task SendConfirmationMail(string destinationEmail, string confirmationCode)
        {
            string emailTemplate = "";
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(EmailTemplates.ConfirmEmailTemplate);
            using (StreamReader reader = new StreamReader(stream))
            {
                emailTemplate = reader.ReadToEnd();
            }
            string mailBody = Engine.Razor.RunCompile(emailTemplate, "confirmMail", null, new { Code = confirmationCode });

            return SendMailAsync(destinationEmail, "Confirma tu cuenta en PracticeJob", mailBody);
        }

        public Task SendPasswordReset(string destinationEmail, string confirmationCode)
        {
            string emailTemplate = "";
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(EmailTemplates.PasswordRecoveryTemplate);
            using (StreamReader reader = new StreamReader(stream))
            {
                emailTemplate = reader.ReadToEnd();
            }
            string mailBody = Engine.Razor.RunCompile(emailTemplate, "resetPassword", null, new {Code = confirmationCode });

            return SendMailAsync(destinationEmail, "Reestablecimiento de contraseña", mailBody);
        }
        public Task SendCompanyContact(ContactMail contactMail)
        {
            string emailTemplate = "";
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(EmailTemplates.CompanyContactTemplate);
            using (StreamReader reader = new StreamReader(stream))
            {
                emailTemplate = reader.ReadToEnd();
            }
            string mailBody = Engine.Razor.RunCompile(emailTemplate, "companyContact", null, new { CompanyName = contactMail.companyName,  Message = contactMail.message });

            return SendMailAsync(contactMail.destinationMail, "Tienes un nuevo mensaje de: " + contactMail.companyName, mailBody);
        }

        public Task SendMailAsync(string destinationEmail, string subject, string bodyMail)
        {
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
            mail.Subject = subject;
            mail.Body = bodyMail;

            smtp.Send(mail);

            return Task.FromResult(true);
        }
    }
}
