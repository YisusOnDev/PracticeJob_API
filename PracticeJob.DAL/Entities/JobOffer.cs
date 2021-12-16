using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeJob.DAL.Entities
{
    public class JobOffer
    {
        [Key]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }
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
