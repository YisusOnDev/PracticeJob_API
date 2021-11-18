using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PracticeJob.Core.DTO;
using System;
using System.Security.Claims;

namespace PracticeJob.Core.Security
{
    public interface ITokenService
    {
        string BuildStudentToken(string key, string issuer, StudentDTO user);
        bool ValidateToken(string key, string issuer, string audience, string token);
    }
}
