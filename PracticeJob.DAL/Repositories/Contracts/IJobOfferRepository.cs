using System.Collections.Generic;
using PracticeJob.DAL.Entities;

namespace PracticeJob.DAL.Repositories.Contracts
{
    public interface IJobOfferRepository
    {
        JobOffer Create(JobOffer offer);
        JobOffer Get(int offerId);
        List<JobOffer> GetAll();
        List<JobOffer> GetAllAvailable();
        List<JobOffer> GetAllFromCompanyId(int companyId);
        List<JobOffer> GetAllFromName(string offerName);
        List<JobOffer> GetAllFromFP(int fpId);
        JobOffer Update(JobOffer offer);
        bool Delete(int offerId);
    }
}
