using PracticeJob.DAL.Entities;

namespace PracticeJob.DAL.Repositories.Contracts
{
    public interface ICompanyRepository
    {
        Company Login(Company company);
        Company Create(Company company);
        Company Update(Company company);
        bool Exists(Company company);
        Company Get(int companyId);
        string Generate2FACode(Company company);
        string Generate2FACode(string email);
        bool Validate2FACode(Company company, string code);
        bool ValidateEmail(Company company);

    }
}
