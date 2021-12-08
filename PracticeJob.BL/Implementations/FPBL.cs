using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;
using PracticeJob.Core.Security;
using PracticeJob.DAL.Entities;
using PracticeJob.DAL.Repositories.Contracts;

namespace PracticeJob.BL.Implementations
{
    public class FPBL : IFPBL
    {
        public IFPRepository fpRepository { get; set; }
        public IMapper mapper { get; set; }

        public FPBL(IFPRepository fpRepository, IMapper mapper)
        {
            this.fpRepository = fpRepository;
            this.mapper = mapper;
        }

        public List<FPDTO> GetAll()
        {
            return mapper.Map<List<FP>, List<FPDTO>>(fpRepository.GetAll());
        }

        public FPDTO Get(int id)
        {
            return mapper.Map<FP, FPDTO>(fpRepository.Get(id));
        }
    }
}
