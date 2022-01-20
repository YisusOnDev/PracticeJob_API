using PracticeJob.Core.DTO;

namespace PracticeJob.BL.Contracts
{
    public interface IStudentBL
    {
        StudentDTO Login(AuthDTO authDTO);
        StudentDTO Create(AuthDTO authDTO);
        StudentDTO Get(int studentId);
        StudentDTO Update(StudentDTO studentDTO);
        string Generate2FACode(StudentDTO studentDTO);
        string Generate2FACode(string email);
        bool Validate2FACode(StudentDTO studentDTO, string code);
        bool ValidateEmail(StudentDTO studentDTO);
    }
}
