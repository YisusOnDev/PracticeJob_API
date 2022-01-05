using AutoMapper;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;
using PracticeJob.DAL.Entities;
using PracticeJob.DAL.Repositories.Contracts;
using System.Collections.Generic;

namespace PracticeJob.BL.Implementations
{
    public class JobApplicationBL : IJobApplicationBL
    {
        public IJobApplicationRepository JobApplicationRepository { get; set; }
        public IMapper Mapper { get; set; }

        public JobApplicationBL(IJobApplicationRepository JobApplicationRepository, IMapper Mapper)
        {
            this.JobApplicationRepository = JobApplicationRepository;
            this.Mapper = Mapper;
        }

        public bool CreateStudentApplication(int jobOfferId, int studentId)
        {
            var succesfull = JobApplicationRepository.CreateStudentApplication(jobOfferId, studentId);
            return succesfull;
        }

        public bool UpdateStudentApplication(int applicationId, ApplicationStatus newStatus)
        {
            var succesfull = JobApplicationRepository.UpdateStudentApplication(applicationId, newStatus);
            return succesfull;
        }
    }
}
