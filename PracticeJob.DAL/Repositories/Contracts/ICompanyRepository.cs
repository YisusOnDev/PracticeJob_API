using System;
using System.Collections.Generic;
using System.Text;
using PracticeJob.DAL.Entities;

namespace PracticeJob.DAL.Repositories.Contracts
{
    public interface ICompanyRepository
    {
        Company Login(Company company);
        Company Create(Company company);
        Company Update(Company company);
        bool Exists(Company company);
        Company Get(int companyId);

    }
}
