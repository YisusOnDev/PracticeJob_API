using PracticeJob.Core.DTO;
using Stripe;

namespace PracticeJob.BL.Contracts
{
    public interface IStripeInvoiceBL
    {
        void RegisterInvoice(Invoice invoice, Subscription subscription);
    }
}
