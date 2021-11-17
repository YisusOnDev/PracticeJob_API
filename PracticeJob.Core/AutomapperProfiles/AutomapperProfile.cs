using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using PracticeJob.Core.DTO;
using PracticeJob.DAL.Entities;

namespace PracticeJob.Core.AutomapperProfiles
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<AuthDTO, Student>();
            CreateMap<Student, AuthDTO>();
            CreateMap<StudentDTO, Student>();
            CreateMap<Student, StudentDTO>();
            CreateMap<ProvinceDTO, Province>();
            CreateMap<Province, ProvinceDTO>();
        }
    }
}
