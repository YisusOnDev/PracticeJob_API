using System;
using System.Collections.Generic;
using System.Text;
using PracticeJob.DAL.Entities;

namespace PracticeJob.DAL.Repositories.Contracts
{
    public interface ICompanyRepository
    {
        Company Login(Company u);
        Company Create(Company u);

        Company Update(Company company);
        bool Exists(Company u);

    }
}
