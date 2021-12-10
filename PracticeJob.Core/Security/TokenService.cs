using Microsoft.IdentityModel.Tokens;
using PracticeJob.Core.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace PracticeJob.Core.Security
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration Configuration;

        public TokenService(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        public string BuildToken(StudentDTO student)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWTSettings:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Id",student.Id.ToString()),
                new Claim("Email", student.Email.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        public string BuildToken(CompanyDTO company)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWTSettings:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Id",company.Id.ToString()),
                new Claim("Email", company.Email.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidToken(string token, StudentDTO student)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token.Replace("Bearer ", string.Empty));
            var tokenS = jsonToken as JwtSecurityToken;
            var id = tokenS.Claims.FirstOrDefault(claim => claim.Type == "Id").Value;
            
            if (id != null)
            {
                if (student.Id == Int32.Parse(id))
                {
                    return true;
                }
            }
            return false;
        }

        public bool ValidToken(string token, CompanyDTO company)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token.Replace("Bearer ", string.Empty));
            var tokenS = jsonToken as JwtSecurityToken;
            var id = tokenS.Claims.FirstOrDefault(claim => claim.Type == "Id").Value;
            if (id != null)
            {
                if (company.Id == Int32.Parse(id) )
                {
                    return true;
                }
            }
            return false;
        }
    }

}
