using System;
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
            return DbContext.JobOffers.
                Include(o => o.Company).
                ThenInclude(p => p.Province).
                Include(o => o.FPs).
                ThenInclude(fp => fp.FPFamily).
                Include(o => o.FPs).
                ThenInclude(fp => fp.FPGrade).
                Include(o => o.JobApplications).
                ThenInclude(app => app.Student).
                Include(o => o.JobApplications).
                ThenInclude(app => app.Student.FP).
                Include(o => o.JobApplications).
                ThenInclude(app => app.Student.Province).
                FirstOrDefault(o => o.Id == offerId);
        }

        public List<JobOffer> GetAll()
        {
            return DbContext.JobOffers.
                Include(o => o.Company).
                ThenInclude(p => p.Province).
                Include(o => o.FPs).
                ThenInclude(fp => fp.FPFamily).
                Include(o => o.FPs).
                ThenInclude(fp => fp.FPGrade).
                Include(o => o.JobApplications).
                ThenInclude(app => app.Student).
                Include(o => o.JobApplications).
                ThenInclude(app => app.Student.FP).
                Include(o => o.JobApplications).
                ThenInclude(app => app.Student.Province).ToList();
        }

        public List<JobOffer> GetAllAvailable()
        {
            return DbContext.JobOffers.
                Include(o => o.Company).
                ThenInclude(p => p.Province).
                Include(o => o.FPs).
                ThenInclude(fp => fp.FPFamily).
                Include(o => o.FPs).
                ThenInclude(fp => fp.FPGrade).
                Where(o => o.EndDate.Date >= DateTime.Today).
                Include(o => o.JobApplications).
                ThenInclude(app => app.Student).
                Include(o => o.JobApplications).
                ThenInclude(app => app.Student.FP).
                Include(o => o.JobApplications).
                ThenInclude(app => app.Student.Province).ToList();
        }

        public List<JobOffer> GetAllAvailableFromFP(int fpId)
        {
            return DbContext.JobOffers.
                Include(o => o.Company).
                ThenInclude(p => p.Province).
                Include(o => o.FPs).
                ThenInclude(fp => fp.FPFamily).
                Include(o => o.FPs).
                ThenInclude(fp => fp.FPGrade).
                Include(o => o.JobApplications).
                ThenInclude(app => app.Student).
                Include(o => o.JobApplications).
                ThenInclude(app => app.Student.FP).
                Include(o => o.JobApplications).
                ThenInclude(app => app.Student.Province).
                Where(o => o.FPs.Any(f => f.Id == fpId) && o.EndDate.Date >= DateTime.Today).ToList();

        }

        public List<JobOffer> GetAllFromCompanyId(int companyId)
        {
            return DbContext.JobOffers.
                Include(o => o.Company).
                ThenInclude(p => p.Province).
                Include(o => o.FPs).
                ThenInclude(fp => fp.FPFamily).
                Include(o => o.FPs).
                ThenInclude(fp => fp.FPGrade).
                Include(o => o.JobApplications).
                ThenInclude(app => app.Student).
                Include(o => o.JobApplications).
                ThenInclude(app => app.Student.FP).
                Include(o => o.JobApplications).
                ThenInclude(app => app.Student.Province).
                Where(o => o.CompanyId == companyId).ToList();
        }

        public List<JobOffer> GetAllFromName(string offerName)
        {
            return DbContext.JobOffers.
                Include(o => o.Company).
                ThenInclude(p => p.Province).
                Include(o => o.FPs).
                ThenInclude(fp => fp.FPFamily).
                Include(o => o.FPs).
                ThenInclude(fp => fp.FPGrade).
                Include(o => o.JobApplications).
                ThenInclude(app => app.Student).
                Include(o => o.JobApplications).
                ThenInclude(app => app.Student.FP).
                Include(o => o.JobApplications).
                ThenInclude(app => app.Student.Province).
                Where(o => o.Name.Contains(offerName)).ToList();
        }

        public List<JobOffer> GetAllFromFP(int fpId)
        {
            return DbContext.JobOffers.
                Include(o => o.Company).
                ThenInclude(p => p.Province).
                Include(o => o.FPs).
                ThenInclude(fp => fp.FPFamily).
                Include(o => o.FPs).
                ThenInclude(fp => fp.FPGrade).
                Include(o => o.JobApplications).
                ThenInclude(app => app.Student).
                Include(o => o.JobApplications).
                ThenInclude(app => app.Student.FP).
                Include(o => o.JobApplications).
                ThenInclude(app => app.Student.Province).
                Where(o => o.FPs.Any(f => f.Id == fpId)).ToList();

        }

        public JobOffer Create(JobOffer offer)
        {
            // Initialize jobapplications
            offer.JobApplications = new List<JobApplication>();

            // Generate offerFpList from FPs Id
            List<FP> offerFpList = new List<FP>();
            foreach(FP f in offer.FPs)
            {
                var fpFromDb = DbContext.FPs.FirstOrDefault(fdb => fdb.Id == f.Id);
                offerFpList.Add(fpFromDb);
            }
            
            // Set offer Fps List with the fp list from DB
            offer.FPs = offerFpList;

            // Get company from db to set that entity to our offer. (prevent errors...).
            var companyFromDb = DbContext.Companies.FirstOrDefault(c => c.Id == offer.CompanyId);
            offer.Company = companyFromDb;
            var offerFromDb = DbContext.JobOffers.Add(offer).Entity;

            DbContext.SaveChanges();

            return offerFromDb;
        }

        public JobOffer Update(JobOffer offer)
        {
            var offerFromDb = Get(offer.Id);
            if (offerFromDb != null)
            {
                // We save current FP List of our offer in a new List in order to remove them correctly.
                
                List<FP> currentOfferFPs = new List<FP>();
                foreach (FP f in offerFromDb.FPs)
                    currentOfferFPs.Add(f);
                   
                foreach (FP f in currentOfferFPs)
                {
                    var fpFromDb = DbContext.FPs.FirstOrDefault(fdb => fdb.Id == f.Id);
                    offerFromDb.FPs.Remove(fpFromDb);
                    
                }

                // Add updated offer fp list
                foreach (FP f in offer.FPs)
                {
                    var fpFromDb = DbContext.FPs.FirstOrDefault(fdb => fdb.Id == f.Id);
                    offerFromDb.FPs.Add(fpFromDb);
                }
                
                offerFromDb.Name = offer.Name;
                offerFromDb.CompanyId = offer.CompanyId;

                // Add company but first we need to select the company from db
                var companyFromDb = DbContext.Companies.FirstOrDefault(c => c.Id == offer.CompanyId);
                offerFromDb.Company = companyFromDb;
                offerFromDb.Description = offer.Description;
                offerFromDb.Remuneration = offer.Remuneration;
                offerFromDb.Schedule = offer.Schedule;
                offerFromDb.StartDate = offer.StartDate;
                offerFromDb.EndDate = offer.EndDate;
                
                DbContext.SaveChanges();
                return offerFromDb;
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
                DbContext.JobOffers.Remove(selectedOffer);
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
