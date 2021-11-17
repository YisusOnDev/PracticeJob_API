using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            return _context.Students.FirstOrDefault<Student>(u => u.Email == user.Email && u.Password == user.Password);
        }

        public Student Create(Student user)
        {
            var u = _context.Students.Add(user);
            _context.SaveChanges();
            return u.Entity;
        }

        public bool Exists(Student user)
        {
            return _context.Students.Any(u => u.Email == user.Email);
        }
    }
}
