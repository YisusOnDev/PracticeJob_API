using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PracticeJob.DAL.Entities;
using PracticeJob.DAL.Repositories.Contracts;

namespace PracticeJob.DAL.Repositories.Implementations
{
    public class StudentRepository : IStudentRepository
    {
        public PracticeJobContext _context { get; set; }
        public StudentRepository(PracticeJobContext context)
        {
            this._context = context;
        }
        public Student Login(Student user)
        {
            return _context.Students.Include(u => u.Province).Include(u => u.FP).ThenInclude(fp => fp.FPFamily).Include(u => u.FP).ThenInclude(fp => fp.FPGrade).FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
        }

        public Student Create(Student user)
        {
            // Put ProvinceId and FPId to 1 to prevent null ForeignKeys error
            user.ProvinceId = 1;
            user.FPId = 1;
            var userFromDb = _context.Students.Add(user).Entity;
            _context.SaveChanges();
            return _context.Students.Include(u => u.Province).Include(u => u.FP).ThenInclude(fp => fp.FPFamily).Include(u => u.FP).ThenInclude(fp => fp.FPGrade).FirstOrDefault(u => u.Email == userFromDb.Email && u.Password == userFromDb.Password);
        }

        public bool Exists(Student user)
        {
            return _context.Students.Any(u => u.Email == user.Email);
        }

        public Student Update(Student student)
        {
            var result = _context.Students.SingleOrDefault(s => s.Email == student.Email);
            if (result != null)
            {
                result.BirthDate = student.BirthDate;
                result.City = student.City;
                result.Name = student.Name;
                result.LastName = student.LastName;
                result.ProvinceId = student.ProvinceId;
                result.ProfileImage = student.ProfileImage;
                result.FPId = student.FPId;
                result.FPCalification = student.FPCalification;

                _context.SaveChanges();
                return _context.Students.Include(u => u.Province).Include(u => u.FP).ThenInclude(fp => fp.FPFamily).Include(u => u.FP).ThenInclude(fp => fp.FPGrade).FirstOrDefault(s => s.Email == student.Email);
            }
            else
            {
                return null;
            }
        }
    }
}
