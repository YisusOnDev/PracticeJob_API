﻿using AutoMapper;
using System.Collections.Generic;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;
using PracticeJob.DAL.Entities;
using PracticeJob.DAL.Repositories.Contracts;

namespace PracticeJob.BL.Implementations
{
    public class ProvinceBL : IProvinceBL
    {
        public IProvinceRepository ProvinceRepository { get; set; }
        public IMapper Mapper { get; set; }

        public ProvinceBL(IProvinceRepository ProvinceRepository, IMapper Mapper)
        {
            this.ProvinceRepository = ProvinceRepository;
            this.Mapper = Mapper;
        }

        public ProvinceDTO Get(int id)
        {
            return Mapper.Map<Province, ProvinceDTO>(ProvinceRepository.Get(id));
        }

        public List<ProvinceDTO> GetAll()
        {
            return Mapper.Map<List<Province>, List<ProvinceDTO>>(ProvinceRepository.GetAll());
        }
    }
}
