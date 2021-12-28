using Microsoft.AspNetCore.Mvc;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

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

        [Authorize]
        [HttpGet]
        [Route("AllAvailable")]
        public ActionResult<JobOfferDTO> GetAllAvailable()
        {
            var offers = JobOfferBL.GetAllAvailable();
            if (offers != null)
            {
                return Ok(offers);
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpGet]
        [Route("AllFromCompany")]
        public ActionResult<JobOfferDTO> GetAllFromCompanyId(int companyId)
        {
            var offers = JobOfferBL.GetAllFromCompanyId(companyId);
            if (offers != null)
            {
                return Ok(offers);
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpGet]
        [Route("AllFromName")]
        public ActionResult<JobOfferDTO> GetAllFromName(string offerName)
        {
            var offers = JobOfferBL.GetAllFromName(offerName);
            if (offers != null)
            {
                return Ok(offers);
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpGet]
        [Route("AllFromFP")]
        public ActionResult<JobOfferDTO> GetAllFromFP(int fpId)
        {
            var offers = JobOfferBL.GetAllFromFP(fpId);
            if (offers != null)
            {
                return Ok(offers);
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize]
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
                return BadRequest();
            }
        }

        [Authorize]
        [HttpDelete]
        public ActionResult<bool> Delete(int offerId)
        {
            bool offer = JobOfferBL.Delete(offerId);
            if (offer)
            {
                return Ok(offer);
            }
            else
            {
                return BadRequest(offer);
            }
        }
    }
}
