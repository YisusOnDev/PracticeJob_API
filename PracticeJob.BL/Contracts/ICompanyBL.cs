using PracticeJob.Core.DTO;

namespace PracticeJob.BL.Contracts
{
    public interface ICompanyBL
    {
        CompanyDTO Login(AuthDTO authDTO);
        CompanyDTO Get(int companyId);
        CompanyDTO Create(AuthDTO authDTO);
        CompanyDTO Update(CompanyDTO companyDTO);
        bool Validate2FACode(CompanyDTO companyDTO, string code);
        void ConfirmEmailSend(CompanyDTO companyDTO);
        bool ValidateEmail(CompanyDTO companyDTO, string code);
    }
}
