using AutoMapper;
using System.Collections.Generic;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;
using PracticeJob.DAL.Entities;
using PracticeJob.DAL.Repositories.Contracts;

namespace PracticeJob.BL.Implementations
{
    public class FPBL : IFPBL
    {
        public IFPRepository FPRepository { get; set; }
        public IMapper Mapper { get; set; }

        public FPBL(IFPRepository FPRepository, IMapper Mapper)
        {
            this.FPRepository = FPRepository;
            this.Mapper = Mapper;
        }

        public FPDTO Get(int id)
        {
            return Mapper.Map<FP, FPDTO>(FPRepository.Get(id));
        }

        public List<FPDTO> GetAll()
        {
            return Mapper.Map<List<FP>, List<FPDTO>>(FPRepository.GetAll());
        }
    }
}
