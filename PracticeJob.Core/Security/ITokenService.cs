using PracticeJob.Core.DTO;

namespace PracticeJob.Core.Security
{
    public interface ITokenService
    {
        string BuildToken(StudentDTO studnet);
        string BuildToken(CompanyDTO company);
        bool ValidToken(string token, StudentDTO student);
        bool ValidToken(string token, CompanyDTO company);
    }

}
