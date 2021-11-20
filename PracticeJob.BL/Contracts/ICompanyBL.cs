using System;
using System.Collections.Generic;
using System.Text;
using PracticeJob.Core.DTO;

namespace PracticeJob.BL.Contracts
{
    public interface ICompanyBL
    {
        CompanyDTO Login(AuthDTO authDTO);
        CompanyDTO Create(AuthDTO authDTO);
        CompanyDTO Update(CompanyDTO companyDTO);
    }
}
