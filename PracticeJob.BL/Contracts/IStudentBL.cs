using PracticeJob.Core.DTO;
using System.Collections.Generic;

namespace PracticeJob.BL.Contracts
{
    public interface IStudentBL
    {
        StudentDTO Login(AuthDTO authDTO);
        StudentDTO Create(AuthDTO authDTO);
        StudentDTO Get(int studentId);
        List<StudentDTO> GetAllFromProvincePremium(int provinceId);
        List<StudentDTO> GetAllFromFpPremium(int fpId);
        List<StudentDTO> GetAllFromFpAndProvincePremium(int fpId, int provinceId);
        StudentDTO Update(StudentDTO studentDTO);
        void ConfirmEmailSend(StudentDTO studentDTO);
        StudentDTO ValidateEmail(StudentDTO studentDTO, string code);
        bool ResetPasswordSend(string email);
        bool UpdatePassword(PasswordResetDTO passwordReset);
        StudentDTO SetProfileImage(int companyId, string fileName);
    }
}
