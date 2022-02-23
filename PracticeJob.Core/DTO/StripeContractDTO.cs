using PracticeJob.DAL.Entities;

namespace PracticeJob.Core.DTO
{
    public class StripeContractDTO
    {
        public string PriceId { get; set; }
        public Company Company { get; set; }
    }
}
