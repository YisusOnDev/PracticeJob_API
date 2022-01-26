using PracticeJob.Core.DTO;

namespace PracticeJob.BL.Contracts
{
    public interface ICompanyBL
    {
        CompanyDTO Login(AuthDTO authDTO);
        CompanyDTO Get(int companyId);
        CompanyDTO Create(AuthDTO authDTO);
        CompanyDTO Update(CompanyDTO companyDTO);
        void ConfirmEmailSend(CompanyDTO companyDTO);
        CompanyDTO ValidateEmail(CompanyDTO companyDTO, string code);
        bool ResetPasswordSend(string email);
        bool UpdatePassword(PasswordResetDTO passwordReset);
        bool ContactStudent(ContactMailDTO contactMailDTO);
        void SetProfileImage(int companyId, string fileName);
    }
}
