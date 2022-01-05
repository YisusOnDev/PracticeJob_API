using System.Collections.Generic;
using PracticeJob.Core.DTO;
using PracticeJob.DAL.Entities;

namespace PracticeJob.BL.Contracts
{
    public interface IJobApplicationBL
    {
        bool CreateStudentApplication(int jobOfferId, int studentId);
        bool UpdateStudentApplication(int applicationId, ApplicationStatus newStatus);
    }
}
