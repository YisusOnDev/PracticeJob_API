using AutoMapper;
using Microsoft.Extensions.Configuration;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;
using PracticeJob.DAL.Entities;
using PracticeJob.DAL.Repositories.Contracts;
using Stripe;

namespace PracticeJob.BL.Implementations
{
    public class StripeInvoiceBL : IStripeInvoiceBL
    {
        public IStripeInvoiceRepository StripeInvoiceRepository { get; set; }
        public IMapper Mapper { get; set; }

        public StripeInvoiceBL(IStripeInvoiceRepository stripeInvoiceRepository, IMapper Mapper)
        {
            this.StripeInvoiceRepository = stripeInvoiceRepository;
            this.Mapper = Mapper;
        }
        public void RegisterInvoice(Invoice invoice, Subscription subscription)
        {
            /// Build invoice db object and insert to db with correct values
            StripeInvoiceDTO invoiceDTO = new StripeInvoiceDTO
            {
                customerId = invoice.CustomerId,
                chargeId = invoice.ChargeId,
                periodStart = subscription.CurrentPeriodStart,
                periodEnd = subscription.CurrentPeriodEnd,
                invoicePdf = invoice.InvoicePdf,
                created = invoice.Created
            };
            StripeInvoice saveInvoice = Mapper.Map<StripeInvoiceDTO, StripeInvoice>(invoiceDTO);
            StripeInvoiceRepository.RegisterInvoice(saveInvoice);
        }
    }
}
