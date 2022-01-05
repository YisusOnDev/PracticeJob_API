using System.Collections.Generic;
using PracticeJob.DAL.Entities;

namespace PracticeJob.DAL.Repositories.Contracts
{
    public interface IJobApplicationRepository
    {
        bool CreateStudentApplication(int jobOfferId, int studentId);
        bool UpdateStudentApplication(int applicationId, ApplicationStatus newStatus);
        bool Exists(int jobOfferId, int studentId);
    }
}
