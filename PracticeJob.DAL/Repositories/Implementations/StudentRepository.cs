using System.Linq;
using Microsoft.EntityFrameworkCore;
using PracticeJob.DAL.Entities;
using PracticeJob.DAL.Repositories.Contracts;

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
            var createdStudent = DbContext.Students.Add(student).Entity;
            DbContext.SaveChanges();
            return createdStudent;
        }

        public bool Exists(Student student)
        {
            return DbContext.Students.Any(s => s.Email == student.Email);
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
            else
            {
                return null;
            }
        }
    }
}
