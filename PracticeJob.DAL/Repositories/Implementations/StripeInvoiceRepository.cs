using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PracticeJob.DAL.Entities;
using PracticeJob.DAL.Repositories.Contracts;

namespace PracticeJob.DAL.Repositories.Implementations
{
    public class StripeInvoiceRepository : IStripeInvoiceRepository
    {
        public PracticeJobContext DbContext { get; set; }
        public StripeInvoiceRepository(PracticeJobContext context)
        {
            this.DbContext = context;
        }
        

        public void RegisterInvoice(StripeInvoice invoice)
        {
            DbContext.Stripeinvoices.Add(invoice);
            DbContext.SaveChanges();
        }

    }
}
