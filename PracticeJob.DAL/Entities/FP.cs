using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PracticeJob.DAL.Entities
{
    public class FP
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int FPGradeId { get; set; }
        [ForeignKey("FPGradeId")]
        public FPGrade FPGrade { get; set; }
        public int FPFamilyId { get; set; }
        [ForeignKey("FPFamilyId")]
        public FPFamily FPFamily { get; set; }

        /* EF N:N */
        public ICollection<JobOffer> JobOffers { get; set; }
    }
}
