using PracticeJob.DAL.Entities;
using System;
using System.Collections.Generic;

namespace PracticeJob.Core.DTO
{
    public class JobOfferDTO
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Remuneration { get; set; }
        public string Schedule { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<FPDTO> FPs { get; set; }
    }
}
