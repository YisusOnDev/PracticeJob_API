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
            var result = DbContext.Companies.SingleOrDefault(c => c.Email == company.Email);
            if (result != null)
            {
                result = company;
                DbContext.SaveChanges();
                return DbContext.Companies.Include(c => c.Province).FirstOrDefault(c => c.Email == company.Email);
            }
            else
            {
                return null;
            }
        }
    }
}
