using System.Collections.Generic;
using PracticeJob.DAL.Entities;

namespace PracticeJob.DAL.Repositories.Contracts
{
    public interface IJobOfferRepository
    {
        JobOffer Create(JobOffer offer);
        JobOffer Get(int offerId);
        List<JobOffer> GetAll();
        JobOffer Update(JobOffer offer);
        bool Delete(int offerId);
    }
}
