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
        public Company Login(Company company)
        {
            return _context.Companies.Include(u => u.Province).FirstOrDefault(c => c.Email == company.Email && c.Password == company.Password);
        }

        public Company Create(Company company)
        {
            // Put ProvinceId to 1 to prevent null ForeignKeys error
            company.ProvinceId = 1;
            var companyFromDb = _context.Companies.Add(company).Entity;
            _context.SaveChanges();
            return _context.Companies.Include(c => c.Province).FirstOrDefault(c => c.Email == companyFromDb.Email && c.Password == companyFromDb.Password);
        }

        public bool Exists(Company company)
        {
            return _context.Companies.Any(c => c.Email == company.Email);
        }

        public Company Update(Company company)
        {
            var result = _context.Companies.SingleOrDefault(c => c.Email == company.Email);
            if (result != null)
            {
                result.Name = company.Name;
                result.Address = company.Address;
                result.ProvinceId = company.ProvinceId;
                _context.SaveChanges();
                return _context.Companies.Include(c => c.Province).FirstOrDefault(c => c.Email == company.Email);

            }
            else
            {
                return null;
            }
        }
    }
}
