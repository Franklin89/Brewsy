using Brewsy.Domain.Entities;
using Brewsy.Domain.Services;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;

namespace Brewsy.Data.Services
{
    public class ChargeService : IChargeService
    {
        private readonly BrewsyContext _brewsyContext;
        private readonly IConfiguration _configuration;

        public ChargeService(BrewsyContext brewsyContext, IConfiguration configuration)
        {
            _brewsyContext = brewsyContext;
            _configuration = configuration;
        }

        public string Charge(Beer beer)
        {
            var requestOptions = new RequestOptions
            {
                ApiKey = _configuration["Stripe:ApiKey"],
                StripeAccount = beer.User.StripeUserId
            };

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions> {
                    new SessionLineItemOptions {
                        Name = beer.Name,
                        Description = beer.Description,
                        Amount = Convert.ToInt32(beer.Price * 100),
                        Currency = "chf",
                        Quantity = 1,
                        Images = new List<string>{ beer.ImageUrl }
                    },
                },
                PaymentIntentData = new SessionPaymentIntentDataOptions
                {
                    CaptureMethod = "manual",
                    ApplicationFeeAmount = Convert.ToInt32((beer.Price * 0.05M) * 100)
                },
                SuccessUrl = "https://localhost:7001/success",
                CancelUrl = "https://localhost:7001/cancel",
            };

            var service = new SessionService();
            Session session = service.Create(options, requestOptions);
            return session.Id;
        }
    }
}
