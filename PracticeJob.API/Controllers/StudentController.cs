using Microsoft.AspNetCore.Mvc;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using PracticeJob.Core.Security;
using System.Threading.Tasks;
using PracticeJob.Core.Common;
using System.IO;
using System.Collections.Generic;
using Stripe;
using Microsoft.Extensions.Configuration;

namespace PracticeJob.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly ITokenService _tokenService;
        public IStudentBL StudentBL { get; set; }

        public StudentController(IConfiguration configuration, ITokenService tokenService, IStudentBL StudentBL)
        {
            Configuration = configuration;
            _tokenService = tokenService;
            this.StudentBL = StudentBL;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<StudentDTO> Get(int studentId)
        {
            StudentDTO student = StudentBL.Get(studentId);
            if (student != null)
            {
                return Ok(student);
            }
            return NotFound();
        }

       [Authorize]
       [HttpPut]
       public ActionResult<StudentDTO> Update(StudentDTO studentDTO)
       {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            if (_tokenService.ValidToken(token, studentDTO))
            {
                StudentDTO student = StudentBL.Update(studentDTO);
                if (student != null)
                {
                    return Ok(student);
                }
                return BadRequest();
            }
            return Unauthorized();
       }

        [Authorize]
        [HttpPost]
        [Route("ValidateEmail")]
        public ActionResult<StudentDTO> ValidateEmail(StudentDTO studentDTO, string code)
        {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            if (_tokenService.ValidToken(token, studentDTO))
            {
                var updStudent = StudentBL.ValidateEmail(studentDTO, code);
                return Ok(updStudent);
            }
            return Unauthorized();
        }

        [Authorize]
        [HttpPost]
        [Route("SendEmailConfirm")]
        public ActionResult<bool> SendEmailConfirm(StudentDTO studentDTO)
        {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            if (_tokenService.ValidToken(token, studentDTO))
            {
                StudentBL.ConfirmEmailSend(studentDTO);
                return Ok(true);
            }
            return Unauthorized();
            
        }

        [HttpPost]
        [Route("SendPasswordReset")]
        public ActionResult<bool> SendPasswordReset(string email)
        {
            var result = StudentBL.ResetPasswordSend(email);
            return Ok(result);
        }

        [HttpPost]
        [Route("UpdatePassword")]
        public ActionResult<bool> UpdatePassword(PasswordResetDTO passwordReset)
        {
            var updStudent = StudentBL.UpdatePassword(passwordReset);
            return Ok(updStudent);
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("UploadImage")]
        public async Task<ActionResult<StudentDTO>> UploadImage(int studentId)
        {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            if (_tokenService.ValidToken(token, studentId))
            {
                var file = Request.Form.Files[0];
                if (file != null)
                {
                    if (CustomUtils.ImageIsValid(file))
                    {
                        string fileName = "student_" + studentId + System.IO.Path.GetExtension(file.FileName);
                        var path = Directory.GetCurrentDirectory();
                        string filePath = Path.Combine(path, "wwwroot", "profile_images", "student", fileName);
                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        var updStudent = StudentBL.SetProfileImage(studentId, fileName);
                        return Ok(updStudent);
                    }
                    return Conflict("Formato de archivo no válido");
                }

                return Conflict("Archivo corrupto");

            }
            return Unauthorized();
        }

        [Authorize]
        [HttpPost]
        [Route("GetAllPremiumProvince")]
        public ActionResult<List<StudentDTO>> GetAllFromProvincePremium(CompanyDTO companyDTO, int provinceId)
        {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            if (_tokenService.ValidToken(token, companyDTO))
            {
                if (CanDoPremiumRequest(companyDTO))
                {
                    List<StudentDTO> allStudents = StudentBL.GetAllFromProvincePremium(provinceId);
                    return Ok(allStudents);
                }            
            }
            return Unauthorized();
        }

        [Authorize]
        [HttpPost]
        [Route("GetAllPremiumFP")]
        public ActionResult<List<StudentDTO>> GetAllFromFPPremium(CompanyDTO companyDTO, int fpId)
        {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            if (_tokenService.ValidToken(token, companyDTO))
            {
                if (CanDoPremiumRequest(companyDTO))
                {
                    List<StudentDTO> allStudents = StudentBL.GetAllFromFpPremium(fpId);
                    return Ok(allStudents);
                }
            }
            return Unauthorized();
        }

        [Authorize]
        [HttpPost]
        [Route("GetAllPremiumFPAndProvince")]
        public ActionResult<List<StudentDTO>> GetAllFromFPAndProvincePremium(CompanyDTO companyDTO, int fpId, int provinceId)
        {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            if (_tokenService.ValidToken(token, companyDTO))
            {
                if (CanDoPremiumRequest(companyDTO))
                {
                    List<StudentDTO> allStudents = StudentBL.GetAllFromFpAndProvincePremium(fpId, provinceId);
                    return Ok(allStudents);
                }
            }
            return Unauthorized();
        }

        [Authorize]
        [HttpPost]
        [Route("Authorized")]
        public ActionResult<StudentDTO> Authorized(StudentDTO studentDTO)
        {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            if (_tokenService.ValidToken(token, studentDTO))
            {
                StudentDTO student = StudentBL.Get(studentDTO.Id);
                if (student != null)
                {
                    return Ok(student);
                }
                
                return BadRequest();
            }
            return Unauthorized();
        }

        private bool CanDoPremiumRequest(CompanyDTO companyDTO)
        {
            string stripeCustomerId = companyDTO.StripeId;
            if (stripeCustomerId == null)
            {
                return false;
            }
            StripeConfiguration.ApiKey = Configuration["Stripe:Secret"];
            SubscriptionListOptions options = new SubscriptionListOptions
            {
                Price = "price_1KUrcgFqZdmFOIAOmBvbDgoq",
                Status = "active",
                Limit = 5,
                Customer = stripeCustomerId,
            };
            SubscriptionService service = new SubscriptionService();

            return service.List(options).Data.Count > 0;
        }
    }
}
