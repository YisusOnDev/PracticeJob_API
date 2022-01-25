using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PracticeJob.DAL.Entities;
using PracticeJob.DAL.Repositories.Contracts;

namespace PracticeJob.DAL.Repositories.Implementations
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        public PracticeJobContext DbContext { get; set; }
        public JobApplicationRepository(PracticeJobContext context)
        {
            this.DbContext = context;
        }

        public bool CreateStudentApplication(int jobOfferId, int studentId)
        {
            if (Exists(jobOfferId, studentId) == false)
            {
                var newApplication = new JobApplication();
                newApplication.StudentId = studentId;
                newApplication.JobOfferId = jobOfferId;
                newApplication.ApplicationDate = DateTime.Now;
                newApplication.ApplicationStatus = ApplicationStatus.Pending;
            
                var createdApplication = DbContext.JobApllications.Add(newApplication).Entity;
                DbContext.SaveChanges();
                if (createdApplication != null)
                {
                return true;
                }
            }
            return false;
        }

        public bool UpdateStudentApplication(int applicationId, ApplicationStatus newStatus)
        {
            var dbJobApplication = DbContext.JobApllications.SingleOrDefault(a => a.Id == applicationId);
            if (dbJobApplication != null)
            {
                dbJobApplication.ApplicationStatus = newStatus;

                DbContext.SaveChanges();

                return true;
            }
            return false;
        }

        public bool Exists(int jobOfferId, int studentId)
        {
            var dbJobApplication = DbContext.JobApllications.SingleOrDefault(a => a.JobOfferId == jobOfferId && a.StudentId == studentId);
            if (dbJobApplication != null)
            {
                return true;
            }
            return false;
        }

        public bool Delete(int applicationId)
        {
            var dbJobApplication = DbContext.JobApllications.SingleOrDefault(a => a.Id == applicationId);
            if (dbJobApplication != null)
            {
                DbContext.JobApllications.Remove(dbJobApplication);
                DbContext.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
