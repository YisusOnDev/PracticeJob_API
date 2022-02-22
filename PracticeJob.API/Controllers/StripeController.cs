﻿using Microsoft.AspNetCore.Mvc;
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
                StripeConfiguration.ApiKey = Configuration["Stripe:Secret"];
                string stripeCustomerId = companyDTO.StripeId;
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
                StripeConfiguration.ApiKey = Configuration["Stripe:Secret"];
                var options = new SessionCreateOptions
                {
                    SuccessUrl = "https://practicejob.yisus.dev/success.html?session_id={CHECKOUT_SESSION_ID}",
                    CancelUrl = "https://practicejob.yisus.dev//failure.html",
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

            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);

                if (stripeEvent.Type == Events.InvoicePaid)
                {
                    var invoice = stripeEvent.Data.Object as Invoice;
                    Console.WriteLine(invoice);
                    //paymentBL.PagoSuccess(invoice);
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionCreated)
                {
                    var subscription = stripeEvent.Data.Object as Subscription;
                    Console.WriteLine(subscription);
                    //paymentBL.SubscriptionCreated(subscription);
                }
                else
                {
                    // Unexpected event type
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }
                return Ok();
            }
            catch (StripeException e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }
    }
}
