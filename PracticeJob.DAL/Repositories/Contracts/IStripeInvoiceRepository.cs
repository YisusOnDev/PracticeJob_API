using PracticeJob.DAL.Entities;

namespace PracticeJob.DAL.Repositories.Contracts
{
    public interface IStripeInvoiceRepository
    {
        void RegisterInvoice(StripeInvoice invoice);

    }
}
