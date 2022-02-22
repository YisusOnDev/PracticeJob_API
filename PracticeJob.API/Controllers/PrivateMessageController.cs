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
    public class PrivateMessageController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public IPrivateMessageBL PrivateMessageBL { get; set; }

        public PrivateMessageController(ITokenService tokenService, IPrivateMessageBL PrivateMessageBL)
        {
            _tokenService = tokenService;
            this.PrivateMessageBL = PrivateMessageBL;
        }

        [Authorize]
        [HttpGet]
        [Route("AllUnread")]
        public ActionResult<List<PrivateMessageDTO>> AllUnread(int studentId)
        {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            if (_tokenService.ValidToken(token, studentId))
            {
                List<PrivateMessageDTO> unreadMessages = PrivateMessageBL.GetAllUnread(studentId);
                if (unreadMessages != null && unreadMessages.Count > 0)
                {
                    return NoContent();
                }
                return Ok(unreadMessages);
            }
            return Unauthorized();
        }

        [Authorize]
        [HttpPost]
        [Route("SendMessage")]
        public ActionResult<bool> SendMessage(PrivateMessageDTO pmDTO)
        {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            if (_tokenService.ValidToken(token, pmDTO.CompanyId))
            {
                bool success = PrivateMessageBL.Send(pmDTO);
                return success;
            }
            return Unauthorized();
        }

        [Authorize]
        [HttpPost]
        [Route("SetRead")]
        public ActionResult<bool> SendMessage(int pmId)
        {
            bool success = PrivateMessageBL.SetAsRead(pmId);
            return success;
        }
    }
}
