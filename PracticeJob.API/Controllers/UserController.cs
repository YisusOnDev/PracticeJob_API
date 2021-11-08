using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;

namespace PracticeJob.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserBL userBL { get; set; }

        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost]
        [Route("Login")]
       public ActionResult Login(UserDTO userDTO)
        {
            
            if (string.IsNullOrEmpty(userDTO.Email) || string.IsNullOrEmpty(userDTO.Password))
            {
                return Unauthorized();
            }

            if (userBL.Login(userDTO))
                return Ok();
            else
                return Unauthorized();
        }
        [HttpPost]
        [Route("Create")]
        public ActionResult<UserDTO> Create(UserDTO userDTO)
        {
            if (string.IsNullOrEmpty(userDTO.Email) || string.IsNullOrEmpty(userDTO.Password))
            {
                return Unauthorized();
            }

            var user =  userBL.Create(userDTO);
            if (user != null)
                return Ok(user);
            else
                return BadRequest();
            
        }
    }
}
