using System.Collections.Generic;
using PracticeJob.Core.DTO;

namespace PracticeJob.BL.Contracts
{
    public interface IJobOfferBL
    {
        JobOfferDTO Get(int offerId);
        List<JobOfferDTO> GetAll();
        List<JobOfferDTO> GetAllAvailable();
        List<JobOfferDTO> GetAllAvailableFromFP(int fpId);
        List<JobOfferDTO> GetAllFromCompanyId(int companyId);
        List<JobOfferDTO> GetAllFromName(string offerName);
        List<JobOfferDTO> GetAllFromFP(int fpId);
        JobOfferDTO Create(JobOfferDTO offer);
        JobOfferDTO Update(JobOfferDTO offer);
        bool Delete(int offerId);
    }
}
