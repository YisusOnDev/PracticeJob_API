using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PracticeJob.DAL.Entities;
using PracticeJob.DAL.Repositories.Contracts;

namespace PracticeJob.DAL.Repositories.Implementations
{
    public class CompanyRepository : ICompanyRepository
    {
        public PracticeJobContext _context { get; set; }
        public CompanyRepository(PracticeJobContext context)
        {
            this._context = context;
        }
        public Company Login(Company user)
        {
            return _context.Companies.Include(u => u.Province).FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
        }

        public Company Create(Company user)
        {
            user.ProvinceId = 1;
            var u = _context.Companies.Add(user);
            _context.SaveChanges();
            return _context.Companies.Include(u => u.Province).FirstOrDefault(u => u.Id == u.Id);
        }

        public bool Exists(Company user)
        {
            return _context.Companies.Any(u => u.Email == user.Email);
        }
    }
}
