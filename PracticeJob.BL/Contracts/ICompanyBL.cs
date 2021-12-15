using PracticeJob.Core.DTO;

namespace PracticeJob.BL.Contracts
{
    public interface ICompanyBL
    {
        CompanyDTO Login(AuthDTO authDTO);
        CompanyDTO Get(int companyId);
        CompanyDTO Create(AuthDTO authDTO);
        CompanyDTO Update(CompanyDTO companyDTO);
    }
}
