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
        public ICompanyRepository companyRepository { get; set; }
        public IPasswordGenerator passwordGenerator { get; set; }
        public IMapper mapper { get; set; }

        public CompanyBL(ICompanyRepository companyRepository, IPasswordGenerator passwordGenerator, IMapper mapper)
        {
            this.companyRepository = companyRepository;
            this.passwordGenerator = passwordGenerator;
            this.mapper = mapper;
        }
        public CompanyDTO Login(AuthDTO authDTO)
        {
            authDTO.Password = passwordGenerator.Hash(authDTO.Password);
            var loginData = mapper.Map<AuthDTO, Company>(authDTO);
            var company = mapper.Map<Company, CompanyDTO>(companyRepository.Login(loginData));
            return company;
        }

        public CompanyDTO Create(AuthDTO authDTO)
        {
            authDTO.Password = passwordGenerator.Hash(authDTO.Password);

            var company = mapper.Map<AuthDTO, Company>(authDTO);

            if (!companyRepository.Exists(company))
            {
                var c = mapper.Map<Company, CompanyDTO>(companyRepository.Create(company));
                c.Password = null;
                return c;
            }

            return null;
        }

        public CompanyDTO Update(CompanyDTO companyDTO)
        {
            var company = mapper.Map<CompanyDTO, Company>(companyDTO);
            var updCompany = mapper.Map<Company, CompanyDTO>(companyRepository.Update(company));
            return updCompany;
        }
    }
}
