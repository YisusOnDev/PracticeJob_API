using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PracticeJob.Core.DTO;
using System;
using System.Security.Claims;

namespace PracticeJob.Core.Security
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, StudentDTO user);
        string BuildToken(string key, string issuer, CompanyDTO user);
        bool ValidateToken(string key, string issuer, string audience, string token);
    }
}
