﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PracticeJob.DAL.Entities
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public string ProfileImage { get; set; }
        [Required]
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int ProvinceId { get; set; }
        [ForeignKey("ProvinceId")]
        public Province Province { get; set; }
        public bool ValidatedEmail { get; set; }
        [JsonIgnore]
        public string TFCode { get; set; }
        public string StripeId { get; set; }
        public ICollection<PrivateMessage> PrivateMessages { get; set; }
    }
}
