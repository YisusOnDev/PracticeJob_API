using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PracticeJob.DAL.Entities
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [JsonIgnore]
        [Required]
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        [Column(TypeName = "Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime BirthDate { get; set; }
        public int ProvinceId { get; set; }
        [ForeignKey("ProvinceId")]
        public Province Province { get; set; }
        public string City { get; set; }
        public string ProfileImage { get; set; }
        public int FPId { get; set; }
        [ForeignKey("FPId")]
        public FP FP { get; set; }
        public double FPCalification { get; set; }

        public bool ValidatedEmail { get; set; }
        [JsonIgnore]
        public string TFCode { get; set; }

    }
}
