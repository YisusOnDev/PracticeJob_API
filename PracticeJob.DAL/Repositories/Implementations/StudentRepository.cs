﻿using System;
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
            return _context.Students.Include(u => u.Province).FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
        }

        public Student Create(Student user)
        {
            // Put ProvinceId to 1 to prevent null ForeignKeys error
            user.ProvinceId = 1;
            var userFromDb = _context.Students.Add(user).Entity;
            _context.SaveChanges();
            return _context.Students.Include(u => u.Province).FirstOrDefault(u => u.Email == userFromDb.Email && u.Password == userFromDb.Password);
        }

        public bool Exists(Student user)
        {
            return _context.Students.Any(u => u.Email == user.Email);
        }
    }
}
