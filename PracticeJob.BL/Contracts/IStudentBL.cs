using PracticeJob.Core.DTO;

namespace PracticeJob.BL.Contracts
{
    public interface IStudentBL
    {
        StudentDTO Login(AuthDTO authDTO);
        StudentDTO Create(AuthDTO authDTO);
        StudentDTO Get(int studentId);
        StudentDTO Update(StudentDTO studentDTO);
    }
}
