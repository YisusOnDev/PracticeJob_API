using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PracticeJob.Core.Email
{
    public interface IEmailSender
    {
        public Task SendConfirmationMail(string destinationEmail, string confirmationCode);
        public Task SendPasswordReset(string destinationEmail, string confirmationCode);
    }
}
