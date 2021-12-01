using System;
using System.Collections.Generic;
using System.Text;
using PracticeJob.Core.DTO;

namespace PracticeJob.BL.Contracts
{
    public interface IStudentBL
    {
        StudentDTO Login(AuthDTO authDTO);
        StudentDTO Create(AuthDTO authDTO);
        StudentDTO Update(StudentDTO studentDTO);
    }
}
