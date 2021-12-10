using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;
using PracticeJob.DAL.Entities;
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
        [Route("GetAll")]
        public ActionResult<FPDTO> GetAll()
        {
            var FPs = FPBL.GetAll();
            if (FPs != null)
                return Ok(FPs);
            else
                return BadRequest();

        }

        [Authorize]
        [HttpGet]
        [Route("Get")]
        public ActionResult<FPDTO> Get(int id)
        {
            var FPs = FPBL.Get(id);
            if (FPs != null)
                return Ok(FPs);
            else
                return BadRequest();
        }
    }
}
