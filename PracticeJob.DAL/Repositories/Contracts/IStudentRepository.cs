using PracticeJob.DAL.Entities;
using System.Collections.Generic;

namespace PracticeJob.DAL.Repositories.Contracts
{
    public interface IStudentRepository
    {
        Student Login(Student student);
        Student Create(Student student);
        Student Update(Student student);
        bool Exists(Student student);
        bool EmailRegistered(string email);
        Student Get(int studentId);
        List<Student> GetAllFromProvince(int provinceId);
        List<Student> GetAllFromFP(int fpId);
        List<Student> GetAllFromFPAndProvince(int fpId, int provinceId);
        string Generate2FACode(string email);
        Student ValidateEmail(Student student, string code);
        bool UpdatePassword(PasswordReset newPassword);
        Student SetProfileImage(int studentId, string fileName);
    }
}
