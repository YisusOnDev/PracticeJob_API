using Microsoft.AspNetCore.Mvc;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using PracticeJob.BL.Implementations;

namespace PracticeJob.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public IStudentBL studentBL { get; set; }

        public StudentController(IStudentBL studentBL)
        {
            this.studentBL = studentBL;
        }

        [Authorize]
        [HttpPost]
        [Route("Update")]
       public ActionResult<StudentDTO> Update(StudentDTO studentDTO)
        {
            
            if (string.IsNullOrEmpty(studentDTO.Email))
            {
                return BadRequest();
            }

            StudentDTO student = studentBL.Update(studentDTO);
            if (student != null)
            { 
                return Ok(student);
            }
            else
            {
                return Unauthorized();
            }
              
        }
    }
}
