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

namespace PracticeJob.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public IStudentBL StudentBL { get; set; }
        public ICompanyBL CompanyBL { get; set; }
        private string generatedToken = null;

        public AuthController(ITokenService tokenService, IStudentBL StudentBL, ICompanyBL CompanyBL)
        {
            _tokenService = tokenService;
            this.StudentBL = StudentBL;
            this.CompanyBL = CompanyBL;
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
                    StudentDTO student = StudentBL.Login(authDTO);
                    if (student != null)
                    {
                        generatedToken = _tokenService.BuildToken(student);
                        return new GenericAPIResponse<Object>(student, generatedToken);
                    } 
                    else
                    {
                        return Unauthorized();
                    }
                case "Company":
                    CompanyDTO company = CompanyBL.Login(authDTO);
                    if (company != null)
                    {
                        generatedToken = _tokenService.BuildToken(company);
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
                    var student = StudentBL.Create(authDTO);
                    if (student != null)
                    {
                        generatedToken = _tokenService.BuildToken(student);
                        return new GenericAPIResponse<Object>(student, generatedToken);
                    }
                    else
                    {
                        return BadRequest();
                    }
                case "Company":
                    var company = CompanyBL.Create(authDTO);
                    if (company != null)
                    {
                        generatedToken = _tokenService.BuildToken(company);
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
