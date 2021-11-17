using System;
using System.Collections.Generic;
using System.Text;
using PracticeJob.DAL.Entities;

namespace PracticeJob.DAL.Repositories.Contracts
{
    public interface IStudentRepository
    {
        Student Login(Student u);
        Student Create(Student u);
        bool Exists(Student u);
    }
}
