using Microsoft.AspNetCore.Mvc;
using System;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;
using PracticeJob.Core.Common;
using PracticeJob.Core.Security;
using PracticeJob.Core.Email;

namespace PracticeJob.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public IStudentBL StudentBL { get; set; }
        public ICompanyBL CompanyBL { get; set; }
        public IEmailSender EmailSender { get; set; }

        public AuthController(ITokenService tokenService, IStudentBL StudentBL, ICompanyBL CompanyBL, IEmailSender EmailSender)
        {
            _tokenService = tokenService;
            this.StudentBL = StudentBL;
            this.CompanyBL = CompanyBL;
            this.EmailSender = EmailSender;
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
                        string generatedToken = _tokenService.BuildToken(student);
                        Response.Headers.Add("Authorization", generatedToken);
                        return new GenericAPIResponse<Object>(student);
                    } 
                    else
                    {
                        return Unauthorized();
                    }
                case "Company":
                    CompanyDTO company = CompanyBL.Login(authDTO);
                    if (company != null)
                    {
                        string generatedToken = _tokenService.BuildToken(company);
                        Response.Headers.Add("Authorization", generatedToken);
                        return new GenericAPIResponse<Object>(company);
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
                        string confirmEmailCode = StudentBL.Generate2FACode(student);
                        EmailSender.SendConfirmationMail(student.Email, student.Name, confirmEmailCode);
                        string generatedToken = _tokenService.BuildToken(student);
                        Response.Headers.Add("Authorization", generatedToken);
                        return new GenericAPIResponse<Object>(student);
                    }
                    else
                    {
                        return BadRequest();
                    }
                case "Company":
                    var company = CompanyBL.Create(authDTO);
                    if (company != null)
                    {
                        string confirmEmailCode = CompanyBL.Generate2FACode(company);
                        EmailSender.SendConfirmationMail(company.Email, company.Name, confirmEmailCode);                    
                        string generatedToken  = _tokenService.BuildToken(company);
                        Response.Headers.Add("Authorization", generatedToken);
                        return new GenericAPIResponse<Object>(company);
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
