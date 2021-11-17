using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;

namespace PracticeJob.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IStudentBL studentBL { get; set; }

        public AuthController(IStudentBL studentBL)
        {
            this.studentBL = studentBL;
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
                    var student = studentBL.Login(authDTO);
                    if (student != null)
                    {
                        student.Password = null;
                        return Ok(student);
                    } 
                    else
                    {
                        return Unauthorized();
                    }
                case "Business":
                    // Do Business Login
                    return Unauthorized();
                default:
                    return Unauthorized();
            }
        }
        [HttpPost]
        [Route("Create")]
        public ActionResult<StudentDTO> Create(AuthDTO authDTO)
        {
            if (string.IsNullOrEmpty(authDTO.Email) || string.IsNullOrEmpty(authDTO.Password))
            {
                return Unauthorized();
            }   

            var student =  studentBL.Create(authDTO);
            if (student != null)
                return Ok(student);
            else
                return BadRequest();
        }
    }
}
