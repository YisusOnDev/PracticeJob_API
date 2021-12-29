using PracticeJob.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeJob.Core.DTO
{
    public class JobApplicationDTO
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public StudentDTO Student { get; set; }
        public int JobOfferId { get; set; }
        public JobOfferDTO JobOffer { get; set; }
        public DateTime ApplicationDate { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; }
    }
}
