using AutoMapper;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;
using PracticeJob.Core.Security;
using PracticeJob.DAL.Entities;
using PracticeJob.DAL.Repositories.Contracts;

namespace PracticeJob.BL.Implementations
{
    public class CompanyBL : ICompanyBL
    {
        public ICompanyRepository CompanyRepository { get; set; }
        public IPasswordGenerator PwdGenerator { get; set; }
        public IMapper Mapper { get; set; }

        public CompanyBL(ICompanyRepository CompanyRepository, IPasswordGenerator PwdGenerator, IMapper Mapper)
        {
            this.CompanyRepository = CompanyRepository;
            this.PwdGenerator = PwdGenerator;
            this.Mapper = Mapper;
        }
        public CompanyDTO Login(AuthDTO authDTO)
        {
            authDTO.Password = PwdGenerator.Hash(authDTO.Password);
            var loginData = Mapper.Map<AuthDTO, Company>(authDTO);
            var company = Mapper.Map<Company, CompanyDTO>(CompanyRepository.Login(loginData));
            return company;
        }

        public CompanyDTO Create(AuthDTO authDTO)
        {
            authDTO.Password = PwdGenerator.Hash(authDTO.Password);

            var company = Mapper.Map<AuthDTO, Company>(authDTO);

            if (!CompanyRepository.Exists(company))
            {
                var c = Mapper.Map<Company, CompanyDTO>(CompanyRepository.Create(company));
                c.Password = null;
                return c;
            }

            return null;
        }

        public CompanyDTO Update(CompanyDTO companyDTO)
        {
            var company = Mapper.Map<CompanyDTO, Company>(companyDTO);
            var updCompany = Mapper.Map<Company, CompanyDTO>(CompanyRepository.Update(company));
            return updCompany;
        }

        public CompanyDTO Get(int companyId)
        {
            var company = Mapper.Map<Company, CompanyDTO>(CompanyRepository.Get(companyId));
            return company;
        }
    }
}
