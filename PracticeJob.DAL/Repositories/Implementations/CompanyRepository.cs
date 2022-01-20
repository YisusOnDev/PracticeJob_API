using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PracticeJob.DAL.Entities;
using PracticeJob.DAL.Repositories.Contracts;

namespace PracticeJob.DAL.Repositories.Implementations
{
    public class CompanyRepository : ICompanyRepository
    {
        public PracticeJobContext DbContext { get; set; }
        public CompanyRepository(PracticeJobContext context)
        {
            this.DbContext = context;
        }
        public Company Login(Company company)
        {
            return DbContext.Companies.Include(u => u.Province).FirstOrDefault(c => c.Email == company.Email && c.Password == company.Password);
        }

        public Company Get(int companyId)
        {
            return DbContext.Companies.Include(u => u.Province).FirstOrDefault(c => c.Id == companyId);
        }

        public Company Create(Company company)
        {
            // Put ProvinceId to 1 to prevent null ForeignKeys error
            company.ProvinceId = 1;
            var companyFromDb = DbContext.Companies.Add(company).Entity;

            DbContext.SaveChanges();

            return companyFromDb;
        }

        public bool Exists(Company company)
        {
            return DbContext.Companies.Any(c => c.Email == company.Email);
        }

        public Company Update(Company company)
        {
            var dbCompany = DbContext.Companies.SingleOrDefault(c => c.Email == company.Email);
            if (dbCompany != null)
            {
                dbCompany.Name = company.Name;
                dbCompany.Address = company.Address;
                dbCompany.ProvinceId = company.ProvinceId;
                dbCompany.Province = company.Province;

                DbContext.SaveChanges();

                return dbCompany;
            }
            else
            {
                return null;
            }
        }
        public string Generate2FACode(Company company)
        {
            var dbCompany = DbContext.Companies.SingleOrDefault(c => c.Email == company.Email);
            if (dbCompany != null)
            {
                var randomCode = new Random().Next(1000, 9999).ToString();
                dbCompany.TFCode = randomCode;
                DbContext.SaveChanges();
                return randomCode;
            }
            return null;
        }
        public string Generate2FACode(string email)
        {
            var dbCompany = DbContext.Companies.SingleOrDefault(c => c.Email == email);
            if (dbCompany != null)
            {
                var randomCode = new Random().Next(1000, 9999).ToString();
                dbCompany.TFCode = randomCode;
                DbContext.SaveChanges();
                return randomCode;
            }
            return null;
        }

        public bool Validate2FACode(Company company, string code)
        {
            var dbCompany = DbContext.Companies.SingleOrDefault(c => c.Email == company.Email);
            if (dbCompany != null)
            {
                var currentValidCode = dbCompany.TFCode;
                dbCompany.TFCode = null;
                DbContext.SaveChanges();
                if (currentValidCode != null && currentValidCode == code)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ValidateEmail(Company company)
        {
            var dbCompany = DbContext.Companies.SingleOrDefault(c => c.Email == company.Email);
            if (dbCompany != null)
            {
                dbCompany.ValidatedEmail = true;
                DbContext.SaveChanges();
            }
            return false;
        }
    }
}
