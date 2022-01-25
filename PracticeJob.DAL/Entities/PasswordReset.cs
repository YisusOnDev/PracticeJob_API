using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeJob.DAL.Entities
{
    public class PasswordReset
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string TFACode { get; set; }
    }
}
