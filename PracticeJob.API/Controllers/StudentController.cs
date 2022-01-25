using Microsoft.AspNetCore.Mvc;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using PracticeJob.Core.Security;

namespace PracticeJob.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public IStudentBL StudentBL { get; set; }

        public StudentController(ITokenService tokenService, IStudentBL StudentBL)
        {
            _tokenService = tokenService;
            this.StudentBL = StudentBL;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<StudentDTO> Get(int studentId)
        {
            StudentDTO student = StudentBL.Get(studentId);
            if (student != null)
            {
                return Ok(student);
            }
            return NotFound();
        }

       [Authorize]
       [HttpPut]
       public ActionResult<StudentDTO> Update(StudentDTO studentDTO)
       {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            if (_tokenService.ValidToken(token, studentDTO))
            {
                StudentDTO student = StudentBL.Update(studentDTO);
                if (student != null)
                {
                    return Ok(student);
                }
                return BadRequest();
            }
            return Unauthorized();
       }

        [Authorize]
        [HttpPost]
        [Route("ValidateEmail")]
        public ActionResult<StudentDTO> ValidateEmail(StudentDTO studentDTO, string code)
        {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            if (_tokenService.ValidToken(token, studentDTO))
            {
                var updStudent = StudentBL.ValidateEmail(studentDTO, code);
                return Ok(updStudent);
            }
            return Unauthorized();
        }

        [Authorize]
        [HttpPost]
        [Route("SendEmailConfirm")]
        public ActionResult<bool> SendEmailConfirm(StudentDTO studentDTO)
        {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            if (_tokenService.ValidToken(token, studentDTO))
            {
                StudentBL.ConfirmEmailSend(studentDTO);
                return Ok(true);
            }
            return Unauthorized();
            
        }

        [HttpPost]
        [Route("SendPasswordReset")]
        public ActionResult<bool> SendPasswordReset(string email)
        {
            var result = StudentBL.ResetPasswordSend(email);
            return Ok(result);
        }

        [HttpPost]
        [Route("UpdatePassword")]
        public ActionResult<bool> UpdatePassword(PasswordResetDTO passwordReset)
        {
            var updStudent = StudentBL.UpdatePassword(passwordReset);
            return Ok(updStudent);
        }

        [Authorize]
        [HttpPost]
        [Route("Authorized")]
        public ActionResult<StudentDTO> Authorized(StudentDTO studentDTO)
        {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            if (_tokenService.ValidToken(token, studentDTO))
            {
                StudentDTO student = StudentBL.Get(studentDTO.Id);
                if (student != null)
                {
                    return Ok(student);
                }
                
                return BadRequest();
            }
            return Unauthorized();
        }
    }
}
