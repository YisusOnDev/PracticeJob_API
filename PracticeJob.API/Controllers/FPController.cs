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
        public IFPBL fpBL { get; set; }

        public FPController(IFPBL fpBL)
        {
            this.fpBL = fpBL;
        }

        [Authorize]
        [HttpGet]
        [Route("GetAll")]
        public ActionResult<FPDTO> GetAll()
        {
            var fps = fpBL.GetAll();
            if (fps != null)
                return Ok(fps);
            else
                return BadRequest();

        }

        [Authorize]
        [HttpGet]
        [Route("Get")]
        public ActionResult<FPDTO> Get(int id)
        {
            var fps = fpBL.Get(id);
            if (fps != null)
                return Ok(fps);
            else
                return BadRequest();
        }
    }
}
