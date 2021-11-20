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
            // Put ProvinceId to 1 to prevent null ForeignKeys error
            user.ProvinceId = 1;
            var u = _context.Companies.Add(user);
            _context.SaveChanges();
            return _context.Companies.Include(u => u.Province).FirstOrDefault(u => u.Id == u.Id);
        }

        public bool Exists(Company user)
        {
            return _context.Companies.Any(u => u.Email == user.Email);
        }

        public Company Update(Company company)
        {
            var result = _context.Companies.SingleOrDefault(c => c.Id == company.Id);
            if (result != null)
            {
                result.Name = company.Name;
                result.Address = company.Address;
                result.ProvinceId = company.ProvinceId;
                _context.SaveChanges();
                return _context.Companies.Include(c => c.Province).FirstOrDefault(c => c.Id == company.Id);

            }
            else
            {
                return null;
            }
        }
    }
}
