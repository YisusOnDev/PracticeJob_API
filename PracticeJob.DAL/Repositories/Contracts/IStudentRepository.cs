﻿using PracticeJob.DAL.Entities;

namespace PracticeJob.DAL.Repositories.Contracts
{
    public interface IStudentRepository
    {
        Student Login(Student student);
        Student Create(Student student);
        Student Update(Student student);
        bool Exists(Student student);
        Student Get(int studentId);
        string Generate2FACode(string email);
        Student ValidateEmail(Student student, string code);
    }
}
