using AutoMapper;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;
using PracticeJob.DAL.Entities;
using PracticeJob.DAL.Repositories.Contracts;
using System.Collections.Generic;

namespace PracticeJob.BL.Implementations
{
    public class JobOfferBL : IJobOfferBL
    {
        public IJobOfferRepository JobOfferRepository { get; set; }
        public IMapper Mapper { get; set; }

        public JobOfferBL(IJobOfferRepository JobOfferRepository, IMapper Mapper)
        {
            this.JobOfferRepository = JobOfferRepository;
            this.Mapper = Mapper;
        }

        public JobOfferDTO Get(int offerId)
        {
            var jobOffer = Mapper.Map<JobOffer, JobOfferDTO>(JobOfferRepository.Get(offerId));
            return jobOffer;
        }

        public List<JobOfferDTO> GetAll()
        {
            return Mapper.Map<List<JobOffer>, List<JobOfferDTO>>(JobOfferRepository.GetAll());
        }

        public JobOfferDTO Create(JobOfferDTO offerDTO)
        {
            var jobOffer = Mapper.Map<JobOfferDTO, JobOffer>(offerDTO);
            var newJobOffer = Mapper.Map<JobOffer, JobOfferDTO>(JobOfferRepository.Create(jobOffer));
            return newJobOffer;
        }
        
        public JobOfferDTO Update(JobOfferDTO offerDTO)
        {
            var jobOffer = Mapper.Map<JobOfferDTO, JobOffer>(offerDTO);
            var updJobOffer = Mapper.Map<JobOffer, JobOfferDTO>(JobOfferRepository.Update(jobOffer));
            return updJobOffer;
        }

    }
}
