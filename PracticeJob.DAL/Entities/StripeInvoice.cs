using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PracticeJob.DAL.Entities
{
    public class StripeInvoice
    {
        [Key]
        public string chargeId { get; set; }
        public string customerId { get; set; }
        public DateTime periodStart { get; set; }
        public DateTime periodEnd { get; set; }
        public DateTime created { get; set; }
    }
}
