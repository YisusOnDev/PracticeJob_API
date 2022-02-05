using System;
using System.Text.Json.Serialization;

namespace PracticeJob.Core.DTO
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int ProvinceId { get; set; }
        public ProvinceDTO Province { get; set; }
        public string City { get; set; }
        public string ProfileImage { get; set; }
        public int FPId { get; set; }
        public FPDTO FP { get; set; }
        public double FPCalification { get; set; }
        public bool ValidatedEmail { get; set; }
    }
}
