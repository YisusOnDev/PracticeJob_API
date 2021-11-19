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
        private string generatedToken = null;

        public AuthController(IConfiguration config, ITokenService tokenService, IStudentBL studentBL)
        {
            _config = config;
            _tokenService = tokenService;
            this.studentBL = studentBL;
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
                        generatedToken = _tokenService.BuildStudentToken(_config["JWTSettings:Secret"].ToString(), _config["JWTSettings:Issuer"].ToString(), student);
                        return new GenericAPIResponse<Object>(student, generatedToken);
                    } 
                    else
                    {
                        return Unauthorized();
                    }
                case "Business":
                    // Do Business Login
                    return Unauthorized();
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
                        return Ok(student);
                    else
                        return BadRequest();
                case "Business":
                    // Do Business Create
                    return Unauthorized();
                default:
                    return BadRequest();
            }

        }
    }
}
