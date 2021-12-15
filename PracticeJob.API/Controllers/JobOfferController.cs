using Microsoft.AspNetCore.Mvc;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;
using Microsoft.AspNetCore.Authorization;

namespace PracticeJob.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobOfferController : ControllerBase
    {
        public IJobOfferBL JobOfferBL { get; set; }

        public JobOfferController(IJobOfferBL JobOfferBL)
        {
            this.JobOfferBL = JobOfferBL;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<JobOfferDTO> Get(int offerId)
        {
            JobOfferDTO offer = JobOfferBL.Get(offerId);
            if (offer != null)
            {
                return Ok(offer);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpGet]
        [Route("All")]
        public ActionResult<JobOfferDTO> GetAll()
        {
            var offers = JobOfferBL.GetAll();
            if (offers != null)
            {
                return Ok(offers);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult<JobOfferDTO> Create(JobOfferDTO offerDTO)
        {
            var offer = JobOfferBL.Create(offerDTO);
            if (offer != null)
            {
                return Ok(offer);
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPut]
        public ActionResult<JobOfferDTO> Update(JobOfferDTO offerDTO)
        {
            JobOfferDTO offer = JobOfferBL.Update(offerDTO);
            if (offer != null)
            {
                return Ok(offer);
            }
            else
            {
                BadRequest();
            }
            return Unauthorized();
        }
    }
}
