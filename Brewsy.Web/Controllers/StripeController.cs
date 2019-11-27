using Brewsy.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe;
using System.Linq;
using System.Security.Claims;

namespace Brewsy.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StripeController : Controller
    {
        private readonly BrewsyContext _brewsyContext;
        private readonly IConfiguration _configuration;

        public StripeController(BrewsyContext brewsyContext, IConfiguration configuration)
        {
            _brewsyContext = brewsyContext;
            _configuration = configuration;
        }

        [HttpGet("[action]")]
        public IActionResult Callback(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                var options = new OAuthTokenCreateOptions
                {
                    GrantType = "authorization_code",
                    Code = code,
                };

                var service = new OAuthTokenService();
                var result = service.Create(options, new RequestOptions
                {
                    ApiKey = _configuration["Stripe:ApiKey"]
                });

                var user = _brewsyContext.Users.FirstOrDefault(x => x.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

                user.StripeUserId = result.StripeUserId;

                _brewsyContext.SaveChanges();
            }

            return RedirectToPage("/beers/index");
        }
    }
}