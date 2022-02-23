using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeJob.Core.DTO
{
    public class PrivateMessageDTO
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int StudentId { get; set; }
        public StudentDTO Student { get; set; }
        public int CompanyId { get; set; }
        public CompanyDTO Company { get; set; }
        public bool Read { get; set; }
    }
}
