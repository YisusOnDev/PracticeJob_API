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
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
        }
    }
}
