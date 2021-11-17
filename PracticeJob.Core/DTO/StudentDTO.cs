using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeJob.Core.DTO
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int ProvinceId { get; set; }
        public ProvinceDTO Province { get; set; }
    }
}
