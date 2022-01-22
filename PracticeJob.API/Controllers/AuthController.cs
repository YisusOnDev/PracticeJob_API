using Microsoft.AspNetCore.Mvc;
using System;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;
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

        public AuthController(ITokenService tokenService, IStudentBL StudentBL, ICompanyBL CompanyBL)
        {
            _tokenService = tokenService;
            this.StudentBL = StudentBL;
            this.CompanyBL = CompanyBL;
        }

        [HttpPost]
        [Route("Login")]
       public ActionResult<Object> Login(AuthDTO authDTO)
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
                        string generatedToken = _tokenService.BuildToken(student);
                        Response.Headers.Add("Authorization", generatedToken);
                        return Ok(student);
                    } 
                    return Unauthorized();

                case "Company":
                    CompanyDTO company = CompanyBL.Login(authDTO);
                    if (company != null)
                    {
                        string generatedToken = _tokenService.BuildToken(company);
                        Response.Headers.Add("Authorization", generatedToken);
                        return Ok(company);
                    }
                    return Unauthorized();

                default:
                    return BadRequest();
            }
        }
        [HttpPost]
        [Route("Create")]
        public ActionResult<Object> Create(AuthDTO authDTO)
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
                        StudentBL.ConfirmEmailSend(student);
                        string generatedToken = _tokenService.BuildToken(student);
                        Response.Headers.Add("Authorization", generatedToken);
                        return Ok(student);
                    }
                    return BadRequest();

                case "Company":
                    var company = CompanyBL.Create(authDTO);
                    if (company != null)
                    {
                        CompanyBL.ConfirmEmailSend(company);
                        string generatedToken  = _tokenService.BuildToken(company);
                        Response.Headers.Add("Authorization", generatedToken);
                        return Ok(company);
                    }
                    return BadRequest();

                default:
                    return BadRequest();
            }

        }
    }
}
