using Microsoft.AspNetCore.Mvc;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;
using Microsoft.AspNetCore.Authorization;

namespace PracticeJob.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProvinceController : ControllerBase
    {
        public IProvinceBL ProvinceBL { get; set; }

        public ProvinceController(IProvinceBL ProvinceBL)
        {
            this.ProvinceBL = ProvinceBL;
        }

        [Authorize]
        [HttpGet]
        [Route("All")]
        public ActionResult<ProvinceDTO> GetAll()
        {
            var provinces =  ProvinceBL.GetAll();
            if (provinces != null)
            {
                return Ok(provinces);
            }
            return BadRequest();
        }

        [Authorize]
        [HttpGet]
        public ActionResult<ProvinceDTO> Get(int provinceId)
        {
            var province = ProvinceBL.Get(provinceId);
            if (province != null)
            {
                return Ok(province);
            }
            return NotFound();
        }
    }
}
