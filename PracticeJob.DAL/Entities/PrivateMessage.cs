using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PracticeJob.DAL.Entities
{
    public class PrivateMessage
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public bool Read { get; set; }
    }
}
