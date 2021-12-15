using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PracticeJob.DAL.Entities;
using PracticeJob.DAL.Repositories.Contracts;

namespace PracticeJob.DAL.Repositories.Implementations
{
    public class JobOfferRepository : IJobOfferRepository
    {
        public PracticeJobContext DbContext { get; set; }
        public JobOfferRepository(PracticeJobContext context)
        {
            this.DbContext = context;
        }

        public JobOffer Get(int offerId)
        {
            return DbContext.JobOffers.Include(o => o.FPs).FirstOrDefault(o => o.Id == offerId);
        }

        public List<JobOffer> GetAll()
        {
            return DbContext.JobOffers.ToList();
        }

        public JobOffer Create(JobOffer offer)
        {
            var offerFromDb = DbContext.JobOffers.Add(offer).Entity;
            DbContext.SaveChanges();
            return DbContext.JobOffers.Include(o => o.FPs).FirstOrDefault(o => o.Id == offerFromDb.Id);
        }

        public JobOffer Update(JobOffer offer)
        {
            var offerFromDb = DbContext.JobOffers.SingleOrDefault(o => o.Id == offer.Id);
            if (offerFromDb != null)
            {
                offerFromDb = offer;
                DbContext.SaveChanges();
                return DbContext.JobOffers.Include(o => o.FPs).FirstOrDefault(o => o.Id == offerFromDb.Id);
            }
            else
            {
                return null;
            }
        }
    }
}
