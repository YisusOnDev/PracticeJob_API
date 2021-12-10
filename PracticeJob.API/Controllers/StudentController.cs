using Microsoft.AspNetCore.Mvc;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using PracticeJob.BL.Implementations;
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
        [HttpPost]
        [Route("Update")]
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
            }
            return Unauthorized();
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
            }
            return Unauthorized();
        }
    }
}
