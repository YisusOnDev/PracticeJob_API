using Microsoft.AspNetCore.Mvc;
using System;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;
using PracticeJob.Core.Security;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using System.Threading.Tasks;
using Stripe;
using Stripe.Checkout;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;

namespace PracticeJob.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController : ControllerBase
    {

        private readonly ITokenService _tokenService;
        public ICompanyBL CompanyBL { get; set; }

        public StripeController(ICompanyBL CompanyBL, ITokenService TokenService)
        {
            this.CompanyBL = CompanyBL;
            this._tokenService = TokenService;
        }

        [Authorize]
        [HttpPost]
        [Route("CreateAccount")]
        public ActionResult<CompanyDTO> CreateStripeCustomer(CompanyDTO companyDTO)
        {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            if (_tokenService.ValidToken(token, companyDTO))
            {
                CompanyDTO _companyDTO = CompanyBL.CreateStripeId(companyDTO);
                if (_companyDTO == null)
                {
                    return BadRequest();
                }
                return Ok(_companyDTO);
            }
            return Unauthorized();

        }

        [Authorize]
        [HttpPost]
        [Route("IsPremium")]
        public ActionResult<CompanyDTO> HasPremiumMemberhsip(CompanyDTO companyDTO)
        {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            if (_tokenService.ValidToken(token, companyDTO))
            {
                return Ok("TODO");
            }
            return Unauthorized();

        }

        [HttpPost]
        [AllowAnonymous]
        [Route("PaySubscription")]
        public ActionResult<string> GenerateSubscriptionPayment(StripeContractDTO contractDTO)
        {
            var options = new SessionCreateOptions
            {
                SuccessUrl = "success.html?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = "/failure.html",
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                Mode = "subscription",
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Price = contractDTO.PriceId, //Your priceID
                                                      // For metered billing, do not pass quantity
                        Quantity = 1,
                    },
                },
                Customer = contractDTO.Company.StripeId,
                SubscriptionData = new SessionSubscriptionDataOptions()
            };

            var service = new SessionService();
            var session = service.Create(options);
            return Ok(new StripePaymentDTO { Url = session.Url });
        }

    }
}
