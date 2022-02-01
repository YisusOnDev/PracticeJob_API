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
        bool EmailRegistered(string email);
        string Generate2FACode(string email);
        Company ValidateEmail(Company company, string code);
        bool UpdatePassword(PasswordReset newPassword);
        Company SetProfileImage(int companyId, string fileName);

    }
}
