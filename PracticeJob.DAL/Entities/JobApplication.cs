using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PracticeJob.DAL.Entities
{
    public enum ApplicationStatus
    {
        Pending,
        Accepted,
        Declined
    }
    public class JobApplication
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; }
        public int JobOfferId { get; set; }
        [ForeignKey("JobOfferId")]
        public JobOffer JobOffer { get; set; }
        public DateTime ApplicationDate { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; }

    }
}
