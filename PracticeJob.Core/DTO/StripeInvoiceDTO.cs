using System;

namespace PracticeJob.Core.DTO
{
    public class StripeInvoiceDTO
    {
        public string customerId { get; set; }
        public string chargeId { get; set; }
        public DateTime periodStart { get; set; }
        public DateTime periodEnd { get; set; }
        public string invoicePdf { get; set; }
        public DateTime created { get; set; }
    }
}
