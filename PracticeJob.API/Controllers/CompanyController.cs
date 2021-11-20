using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;
using PracticeJob.Core.Common;
using PracticeJob.Core.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace PracticeJob.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        public ICompanyBL companyBL { get; set; }

        public CompanyController(ICompanyBL companyBL)
        {
            this.companyBL = companyBL;
        }

        [Authorize]
        [HttpPost]
        [Route("Update")]
       public ActionResult<CompanyDTO> Update(CompanyDTO companyDTO)
        {
            
            if (string.IsNullOrEmpty(companyDTO.Email))
            {
                return BadRequest();
            }

            CompanyDTO company = companyBL.Update(companyDTO);
            if (company != null)
            { 
                return Ok(company);
            }
            else
            {
                return Unauthorized();
            }
              
        }
    }
}
