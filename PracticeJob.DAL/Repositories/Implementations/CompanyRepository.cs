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
        public bool EmailRegistered(string email)
        {
            return DbContext.Companies.Any(s => s.Email == email);
        }

        public string Generate2FACode(string email)
        {
            var companyFromDb = DbContext.Companies.SingleOrDefault(s => s.Email == email);
            if (companyFromDb != null)
            {
                var randomCode = new Random().Next(1000, 9999).ToString();
                companyFromDb.TFCode = randomCode;
                DbContext.SaveChanges();
                return randomCode;
            }
            return null;
        }

        public Company ValidateEmail(Company company, string code)
        {
            var companyFromDb = DbContext.Companies.SingleOrDefault(s => s.Email == company.Email);
            if (companyFromDb != null)
            {
                if (companyFromDb.TFCode == code)
                {
                    companyFromDb.ValidatedEmail = true;
                    companyFromDb.TFCode = null;
                }
                DbContext.SaveChanges();
                return companyFromDb;
            }
            return null;
        }

        public bool UpdatePassword(PasswordReset newPassword)
        {
            var companyFromDb = DbContext.Companies.SingleOrDefault(s => s.Email == newPassword.Email);
            if (companyFromDb != null)
            {
                if (companyFromDb.TFCode == newPassword.TFACode)
                {
                    companyFromDb.Password = newPassword.Password;
                    companyFromDb.TFCode = null;

                    DbContext.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
