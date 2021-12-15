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
using Microsoft.AspNetCore.Authentication;

namespace PracticeJob.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public ICompanyBL CompanyBL { get; set; }

        public CompanyController(ITokenService tokenService, ICompanyBL CompanyBL)
        {
            _tokenService = tokenService;
            this.CompanyBL = CompanyBL;
        }

        [Authorize]
        [HttpPut]
       public ActionResult<CompanyDTO> Update(CompanyDTO companyDTO)
       {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            if (_tokenService.ValidToken(token, companyDTO))
            {
                CompanyDTO company = CompanyBL.Update(companyDTO);
                if (company != null)
                {
                    return Ok(company);
                }
            }
            return Unauthorized();
       }

        [Authorize]
        [HttpGet]
        public ActionResult<CompanyDTO> Get(int companyId)
        {
            CompanyDTO company = CompanyBL.Get(companyId);
            if (company != null)
            {
                return Ok(company);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpPost]
        [Route("Authorized")]
        public ActionResult<StudentDTO> Authorized(CompanyDTO companyDTO)
        {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            if (_tokenService.ValidToken(token, companyDTO))
            {
                CompanyDTO company = CompanyBL.Get(companyDTO.Id);
                if (company != null)
                {
                    return Ok(company);
                }
            }
            return Unauthorized();
        }
    }
}
