using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PracticeJob.Core.Email
{
    public interface IEmailSender
    {
        public void SendConfirmationMail(string destinationEmail, string name, string confirmationCode);
    }
}
