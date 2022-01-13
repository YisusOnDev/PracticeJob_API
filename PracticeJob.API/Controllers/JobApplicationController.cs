using Microsoft.AspNetCore.Mvc;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using PracticeJob.DAL.Entities;

namespace PracticeJob.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobApplicationController : ControllerBase
    {
        public IJobApplicationBL JobApplicationBL { get; set; }

        public JobApplicationController(IJobApplicationBL JobApplicationBL)
        {
            this.JobApplicationBL = JobApplicationBL;
        }

        [Authorize]
        [HttpPost]
        public ActionResult<bool> CreateStudentApplication(int jobOfferId, int studentId)
        {
            bool success = JobApplicationBL.CreateStudentApplication(jobOfferId, studentId);
            if (success == true || success == false)
            {
                return Ok(success);
            } 
            else
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateStudentApplication")]
        public ActionResult<bool> UpdateStudentApplication(int applicationId, ApplicationStatus newStatus)
        {
            bool changed = JobApplicationBL.UpdateStudentApplication(applicationId, newStatus);
            if (changed == true || changed == false)
            {
                return Ok(changed);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
