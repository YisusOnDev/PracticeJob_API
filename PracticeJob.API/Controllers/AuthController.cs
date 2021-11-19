using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;
using PracticeJob.Core.Common;
using PracticeJob.Core.Security;
using Microsoft.Extensions.Configuration;

namespace PracticeJob.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        public IStudentBL studentBL { get; set; }
        public ICompanyBL companyBL { get; set; }
        private string generatedToken = null;

        public AuthController(IConfiguration config, ITokenService tokenService, IStudentBL studentBL, ICompanyBL companyBL)
        {
            _config = config;
            _tokenService = tokenService;
            this.studentBL = studentBL;
            this.companyBL = companyBL;
        }

        [HttpPost]
        [Route("Login")]
       public ActionResult<GenericAPIResponse<Object>> Login(AuthDTO authDTO)
        {
            
            if (string.IsNullOrEmpty(authDTO.Email) || string.IsNullOrEmpty(authDTO.Password))
            {
                return Unauthorized();
            }

            switch (authDTO.LoginType)
            {
                case "Student":
                    StudentDTO student = studentBL.Login(authDTO);
                    if (student != null)
                    {
                        generatedToken = _tokenService.BuildToken(_config["JWTSettings:Secret"].ToString(), _config["JWTSettings:Issuer"].ToString(), student);
                        return new GenericAPIResponse<Object>(student, generatedToken);
                    } 
                    else
                    {
                        return Unauthorized();
                    }
                case "Company":
                    CompanyDTO company = companyBL.Login(authDTO);
                    if (company != null)
                    {
                        generatedToken = _tokenService.BuildToken(_config["JWTSettings:Secret"].ToString(), _config["JWTSettings:Issuer"].ToString(), company);
                        return new GenericAPIResponse<Object>(company, generatedToken);
                    }
                    else
                    {
                        return Unauthorized();
                    }
                default:
                    return BadRequest();
            }
        }
        [HttpPost]
        [Route("Create")]
        public ActionResult<GenericAPIResponse<Object>> Create(AuthDTO authDTO)
        {
            if (string.IsNullOrEmpty(authDTO.Email) || string.IsNullOrEmpty(authDTO.Password))
            {
                return BadRequest();
            }

            switch (authDTO.LoginType)
            {
                case "Student":
                    var student = studentBL.Create(authDTO);
                    if (student != null)
                    {
                        generatedToken = _tokenService.BuildToken(_config["JWTSettings:Secret"].ToString(), _config["JWTSettings:Issuer"].ToString(), student);
                        return new GenericAPIResponse<Object>(student, generatedToken);
                    }
                    else
                    {
                        return BadRequest();
                    }
                case "Company":
                    var company = companyBL.Create(authDTO);
                    if (company != null)
                    {
                        generatedToken = _tokenService.BuildToken(_config["JWTSettings:Secret"].ToString(), _config["JWTSettings:Issuer"].ToString(), company);
                        return new GenericAPIResponse<Object>(company, generatedToken);
                    }
                    else
                    {
                        return BadRequest();
                    }
                default:
                    return BadRequest();
            }

        }
    }
}
