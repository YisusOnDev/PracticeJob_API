﻿using Microsoft.AspNetCore.Http;
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
    public class ProvinceController : ControllerBase
    {
        public IProvinceBL provinceBL { get; set; }

        public ProvinceController(IProvinceBL provinceBL)
        {
            this.provinceBL = provinceBL;
        }

        [Authorize]
        [HttpGet]
        [Route("GetAll")]
        public ActionResult<ProvinceDTO> GetAll()
        {
            var provinces =  provinceBL.GetAll();
            if (provinces != null)
                return Ok(provinces);
            else
                return BadRequest();
            
        }

        [Authorize]
        [HttpGet]
        [Route("Get")]
        public ActionResult<ProvinceDTO> Get(int id)
        {
            var province = provinceBL.Get(id);
            if (province != null)
                return Ok(province);
            else
                return BadRequest();
        }
    }
}
