using System.Linq;
using Microsoft.EntityFrameworkCore;
using PracticeJob.DAL.Entities;
using PracticeJob.DAL.Repositories.Contracts;
using System;

namespace PracticeJob.DAL.Repositories.Implementations
{
    public class StudentRepository : IStudentRepository
    {
        public PracticeJobContext DbContext { get; set; }
        public StudentRepository(PracticeJobContext context)
        {
            this.DbContext = context;
        }
        public Student Login(Student user)
        {
            return DbContext.Students.
                Include(u => u.Province).
                Include(u => u.FP).ThenInclude(fp => fp.FPFamily).
                Include(u => u.FP).ThenInclude(fp => fp.FPGrade).
                FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
        }

        public Student Get(int studentId)
        {
            return DbContext.Students.
                Include(u => u.Province).
                Include(u => u.FP).ThenInclude(fp => fp.FPFamily).
                Include(u => u.FP).ThenInclude(fp => fp.FPGrade).
                FirstOrDefault(u => u.Id == studentId);
        }

        public Student Create(Student student)
        {
            // Put ProvinceId and FPId to 1 to prevent null ForeignKeys error
            student.ProvinceId = 1;
            student.FPId = 1;
            student.ProfileImage = "default.png";
            var createdStudent = DbContext.Students.Add(student).Entity;
            DbContext.SaveChanges();
            return createdStudent;
        }

        public bool Exists(Student student)
        {
            return DbContext.Students.Any(s => s.Email == student.Email);
        }

        public bool EmailRegistered(string email)
        {
            return DbContext.Students.Any(s => s.Email == email);
        }

        public Student Update(Student student)
        {
            var studentFromDb = DbContext.Students.SingleOrDefault(s => s.Email == student.Email);
            if (studentFromDb != null)
            {
                studentFromDb.BirthDate = student.BirthDate;
                studentFromDb.City = student.City;
                studentFromDb.Name = student.Name;
                studentFromDb.LastName = student.LastName;
                studentFromDb.ProvinceId = student.ProvinceId;
                studentFromDb.ProfileImage = student.ProfileImage;
                studentFromDb.FPId = student.FPId;
                studentFromDb.FPCalification = student.FPCalification;

                DbContext.SaveChanges();

                return studentFromDb;
            }
            return null;
        }

        public string Generate2FACode(string email)
        {
            var studentFromDb = DbContext.Students.SingleOrDefault(s => s.Email == email);
            if (studentFromDb != null)
            {
                var randomCode = new Random().Next(1000, 9999).ToString();
                studentFromDb.TFCode = randomCode;
                DbContext.SaveChanges();
                return randomCode;
            }
            return null;
        }

        public Student ValidateEmail(Student student, string code)
        {
            var studentFromDb = DbContext.Students.SingleOrDefault(s => s.Email == student.Email);
            if (studentFromDb != null)
            {
                if (studentFromDb.TFCode == code)
                {
                    studentFromDb.ValidatedEmail = true;
                    studentFromDb.TFCode = null;
                }
                DbContext.SaveChanges();
                return studentFromDb;
            }
            return null;
        }

        public bool UpdatePassword(PasswordReset newPassword)
        {
            var studentFromDb = DbContext.Students.SingleOrDefault(s => s.Email == newPassword.Email);
            if (studentFromDb != null)
            {
                if (studentFromDb.TFCode == newPassword.TFACode)
                {
                    studentFromDb.Password = newPassword.Password;
                    studentFromDb.TFCode = null;
                    
                    DbContext.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public Student SetProfileImage(int studentId, string fileName)
        {
            var studentFromDb = Get(studentId);
            if (studentFromDb != null)
            {
                studentFromDb.ProfileImage = fileName;
                DbContext.SaveChanges();
                return studentFromDb;
            }
            return null;
        }
    }
}
