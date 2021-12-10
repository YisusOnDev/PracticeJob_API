using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace PracticeJob.DAL.Entities
{
    public class JobOffer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Range(0, int.MaxValue)]
        public int Remuneration { get; set; }
        public string Schedule { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        /* EF N:N */
        public ICollection<FP> FPs { get; set; }
    }
}
