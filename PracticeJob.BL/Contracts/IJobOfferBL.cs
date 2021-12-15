using System.Collections.Generic;
using PracticeJob.Core.DTO;

namespace PracticeJob.BL.Contracts
{
    public interface IJobOfferBL
    {
        JobOfferDTO Get(int offerId);
        List<JobOfferDTO> GetAll();
        JobOfferDTO Create(JobOfferDTO offer);
        JobOfferDTO Update(JobOfferDTO offer);
        bool Delete(int offerId);
    }
}
