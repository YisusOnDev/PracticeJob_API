using Microsoft.AspNetCore.Mvc;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;
using Microsoft.AspNetCore.Authorization;

namespace PracticeJob.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FPController : ControllerBase
    {
        public IFPBL FPBL { get; set; }

        public FPController(IFPBL FPBL)
        {
            this.FPBL = FPBL;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<FPDTO> Get(int fpId)
        {
            var FPs = FPBL.Get(fpId);
            if (FPs != null)
            {
                return Ok(FPs);
            }
            return NotFound();
        }

        [Authorize]
        [HttpGet]
        [Route("All")]
        public ActionResult<FPDTO> GetAll()
        {
            var FPs = FPBL.GetAll();
            if (FPs != null)
            {
                return Ok(FPs);
            }
            return BadRequest();
        }
    }
}
