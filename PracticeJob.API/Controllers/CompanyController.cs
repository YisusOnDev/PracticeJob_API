using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;
using PracticeJob.Core.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using PracticeJob.Core.Common;
using System.Linq;

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
        [HttpGet]
        public ActionResult<CompanyDTO> Get(int companyId)
        {
            CompanyDTO company = CompanyBL.Get(companyId);
            if (company != null)
            {
                return Ok(company);
            }
            return NotFound();
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
        [HttpPost]
        [Route("ValidateEmail")]
        public ActionResult<CompanyDTO> ValidateEmail(CompanyDTO companyDTO, string code)
        {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            if (_tokenService.ValidToken(token, companyDTO))
            {
                var updCompany = CompanyBL.ValidateEmail(companyDTO, code);
                return Ok(updCompany);
            }
            return Unauthorized();
        }

        [Authorize]
        [HttpPost]
        [Route("SendEmailConfirm")]
        public ActionResult<bool> SendEmailConfirm(CompanyDTO companyDTO)
        {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            if (_tokenService.ValidToken(token, companyDTO))
            {
                CompanyBL.ConfirmEmailSend(companyDTO);
                return Ok(true);
            }
            return Unauthorized();

        }

        [HttpPost]
        [Route("SendPasswordReset")]
        public ActionResult<bool> SendPasswordReset(string email)
        {
            var result = CompanyBL.ResetPasswordSend(email);
            return Ok(result);
        }

        [HttpPost]
        [Route("UpdatePassword")]
        public ActionResult<bool> UpdatePassword(PasswordResetDTO passwordReset)
        {
            var updStudent = CompanyBL.UpdatePassword(passwordReset);
            return Ok(updStudent);
        }

        [HttpPost]
        [Route("ContactStudent")]
        public ActionResult<bool> ContactStudent(ContactMailDTO contactMailDTO)
        {
            var result = CompanyBL.ContactStudent(contactMailDTO);
            return Ok(result);
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("UploadImage")]
        public async Task<ActionResult<CompanyDTO>> UploadImage(int companyId)
        {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            if (_tokenService.ValidToken(token, companyId))
            {
                IFormFile file = Request.Form.Files.First();
                if (file != null)
                {
                    if (CustomUtils.ImageIsValid(file))
                    {
                        string fileName = "company_" + companyId + System.IO.Path.GetExtension(file.FileName);
                        string path = Directory.GetCurrentDirectory();
                        string filePath = Path.Combine(path, "wwwroot", "profile_images", "company", fileName);
                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        var updCompany = CompanyBL.SetProfileImage(companyId, fileName);
                        return Ok(updCompany);
                    }
                    return Conflict("Formato de archivo no válido");
                }

                return Conflict("Archivo corrupto");

            }
            return Unauthorized();
        }

        [Authorize]
        [HttpPost]
        [Route("Authorized")]
        public ActionResult<CompanyDTO> Authorized(CompanyDTO companyDTO)
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
