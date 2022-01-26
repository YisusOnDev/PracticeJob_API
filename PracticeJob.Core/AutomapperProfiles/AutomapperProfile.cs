using AutoMapper;
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

            CreateMap<AuthDTO, Company>();
            CreateMap<Company, AuthDTO>();
            CreateMap<CompanyDTO, Company>();
            CreateMap<Company, CompanyDTO>();

            CreateMap<ProvinceDTO, Province>();
            CreateMap<Province, ProvinceDTO>();

            CreateMap<FPDTO, FP>();
            CreateMap<FP, FPDTO>();

            CreateMap<JobOfferDTO, JobOffer>();
            CreateMap<JobOffer, JobOfferDTO>();

            CreateMap<JobApplicationDTO, JobApplication>();
            CreateMap<JobApplication, JobApplicationDTO>();

            CreateMap<PasswordResetDTO, PasswordReset>();
            CreateMap<PasswordReset, PasswordResetDTO>();

            CreateMap<ContactMailDTO, ContactMail>();
            CreateMap<ContactMail, ContactMailDTO>();
        }
    }
}
