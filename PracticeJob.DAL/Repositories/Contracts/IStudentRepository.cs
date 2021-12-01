using System;
using System.Collections.Generic;
using System.Text;
using PracticeJob.DAL.Entities;

namespace PracticeJob.DAL.Repositories.Contracts
{
    public interface IStudentRepository
    {
        Student Login(Student student);
        Student Create(Student student);
        Student Update(Student student);
        bool Exists(Student student);
    }
}
