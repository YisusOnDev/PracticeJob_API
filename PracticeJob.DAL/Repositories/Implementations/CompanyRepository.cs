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

            return DbContext.Companies.Include(c => c.Province).FirstOrDefault(c => c.Email == companyFromDb.Email && c.Password == companyFromDb.Password);
        }

        public bool Exists(Company company)
        {
            return DbContext.Companies.Any(c => c.Email == company.Email);
        }

        public Company Update(Company company)
        {
            var dbCompany = DbContext.Companies.SingleOrDefault(c => c.Id == company.Id);
            if (dbCompany != null)
            {
                dbCompany.Name = company.Name;
                dbCompany.Address = company.Address;
                dbCompany.ProvinceId = company.ProvinceId;
                dbCompany.Province = company.Province;

                DbContext.SaveChanges();

                return DbContext.Companies.Include(c => c.Province).FirstOrDefault(c => c.Id == company.Id);
            }
            else
            {
                return null;
            }
        }
    }
}
