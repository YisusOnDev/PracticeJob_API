using PracticeJob.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PracticeJob.Core.DTO
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int ProvinceId { get; set; }
        public Province Province { get; set; }
    }
}
