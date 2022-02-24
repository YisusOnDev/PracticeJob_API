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
        private readonly IConfiguration Configuration;
        private readonly ITokenService _tokenService;
        public ICompanyBL CompanyBL { get; set; }

        public StripeController(ICompanyBL CompanyBL, ITokenService TokenService, IConfiguration Configuration)
        {
            StripeConfiguration.ApiKey = Configuration["Stripe:Secret"];
            this.CompanyBL = CompanyBL;
            this._tokenService = TokenService;
            this.Configuration = Configuration;
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

        [HttpPost]
        [Authorize]
        [Route("IsPremium")]
        public ActionResult<bool> HasPremiumMemberhsip(CompanyDTO companyDTO)
        {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            if (_tokenService.ValidToken(token, companyDTO))
            {
                string stripeCustomerId = companyDTO.StripeId;
                if (stripeCustomerId == null)
                {
                    return false;
                }
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
            return Unauthorized();
        }

        [HttpPost]
        [Authorize]
        [Route("GenerateSubscriptionLink")]
        public ActionResult<string> GeneratePremiumPayLink(CompanyDTO companyDTO)
        {
            var token = HttpContext.GetTokenAsync("access_token").Result;
            if (_tokenService.ValidToken(token, companyDTO))
            {
                if (companyDTO.StripeId == null)
                {
                    companyDTO.StripeId = CompanyBL.CreateStripeId(companyDTO).StripeId;
                }

                var options = new SessionCreateOptions
                {
                    SuccessUrl = "https://practicejob.yisus.dev/paymentSuccess?session_id={CHECKOUT_SESSION_ID}",
                    CancelUrl = "https://practicejob.yisus.dev/paymentFailure",
                    PaymentMethodTypes = new List<string>
                    {
                        "card",
                    },
                    Mode = "subscription",
                    LineItems = new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            Price = "price_1KUrcgFqZdmFOIAOmBvbDgoq",
                            Quantity = 1,
                        },
                    },
                    Customer = companyDTO.StripeId,
                    SubscriptionData = new SessionSubscriptionDataOptions()
                };

                var service = new SessionService();
                var session = service.Create(options);
                return Ok(new StripePaymentDTO { Url = session.Url });
            }
            return Unauthorized();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("webhook")]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            if (json == null)
            {
                return StatusCode(500);
            }
            string endpointSecret = Configuration["Stripe:WebhookSigningSecret"];
            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);
                var signatureHeader = Request.Headers["Stripe-Signature"];

                stripeEvent = EventUtility.ConstructEvent(json,
                        signatureHeader, endpointSecret);

                switch (stripeEvent.Type)
                {
                    case Events.InvoicePaid:
                        var invoice = stripeEvent.Data.Object as Invoice;
                        Console.WriteLine(invoice);
                        //paymentBL.PagoSuccess(invoice);
                        break;
                    case Events.CustomerSubscriptionCreated:
                        var subscription = stripeEvent.Data.Object as Subscription;
                        Console.WriteLine(subscription);
                        //paymentBL.SubscriptionCreated(subscription);
                        break;
                    default:
                        Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                        break;
                }
                return Ok();
            }
            catch (StripeException e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return StatusCode(500);
            }
        }
    }
}
