using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeJob.Core.DTO
{
    public class PasswordResetDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string TFACode { get; set; }
    }
}
