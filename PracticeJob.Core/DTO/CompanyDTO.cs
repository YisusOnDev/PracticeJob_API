using PracticeJob.DAL.Entities;
using System.Text.Json.Serialization;

namespace PracticeJob.Core.DTO
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int ProvinceId { get; set; }
        public ProvinceDTO Province { get; set; }
        public bool ValidatedEmail { get; set; }
    }
}
