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
            return DbContext.JobOffers.Include(o => o.FPs).
                ThenInclude(fp => fp.FPFamily).
                Include(o => o.FPs).
                ThenInclude(fp => fp.FPGrade).
                FirstOrDefault(o => o.Id == offerId);
        }

        public List<JobOffer> GetAll()
        {
            return DbContext.JobOffers.Include(o => o.FPs).
                ThenInclude(fp => fp.FPFamily).
                Include(o => o.FPs).
                ThenInclude(fp => fp.FPGrade).ToList();
        }

        public JobOffer Create(JobOffer offer)
        {
            // Generate offerFpList from FPs Id
            List<FP> offerFpList = new List<FP>();
            foreach(FP f in offer.FPs)
            {
                var fpFromDb = DbContext.FPs.FirstOrDefault(fdb => fdb.Id == f.Id);
                offerFpList.Add(fpFromDb);
            }

            // Set offer Fps List with the fp list from DB
            offer.FPs = offerFpList;
            var offerFromDb = DbContext.JobOffers.Add(offer).Entity;

            DbContext.SaveChanges();

            return DbContext.JobOffers.Include(o => o.FPs).
                ThenInclude(fp => fp.FPFamily).
                Include(o => o.FPs).
                ThenInclude(fp => fp.FPGrade).
                FirstOrDefault(o => o.Id == offerFromDb.Id);
        }

        public JobOffer Update(JobOffer offer)
        {
            var offerFromDb = DbContext.JobOffers.SingleOrDefault(o => o.Id == offer.Id);
            if (offerFromDb != null)
            {
                offerFromDb.Name = offer.Name;
                offerFromDb.Description = offer.Description;
                offerFromDb.Remuneration = offer.Remuneration;
                offerFromDb.Schedule = offer.Schedule;
                offerFromDb.StartDate = offer.StartDate;
                offerFromDb.EndDate = offer.EndDate;
                offerFromDb.FPs = offer.FPs;

                DbContext.SaveChanges();

                return DbContext.JobOffers.Include(o => o.FPs).
                ThenInclude(fp => fp.FPFamily).
                Include(o => o.FPs).
                ThenInclude(fp => fp.FPGrade).
                FirstOrDefault(o => o.Id == offerFromDb.Id);
            }
            else
            {
                return null;
            }
        }

        public bool Delete(int offerId)
        {
            var selectedOffer = DbContext.JobOffers.SingleOrDefault(o => o.Id == offerId);
            if (selectedOffer != null)
            {
                DbContext.JobOffers.Remove(selectedOffer)
                DbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
