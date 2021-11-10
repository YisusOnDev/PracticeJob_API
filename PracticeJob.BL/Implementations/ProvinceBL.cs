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
    public class ProvinceBL : IProvinceBL
    {
        public IProvinceRepository provinceRepository { get; set; }
        public IPasswordGenerator passwordGenerator { get; set; }
        public IMapper mapper { get; set; }

        public ProvinceBL(IProvinceRepository provinceRepository, IMapper mapper)
        {
            this.provinceRepository = provinceRepository;
            this.mapper = mapper;
        }

        public List<ProvinceDTO> Get()
        {
            return mapper.Map<Province, ProvinceDTO>(provinceRepository.Get()); ;
        }
    }
}
