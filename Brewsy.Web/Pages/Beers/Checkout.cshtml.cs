using Brewsy.Data;
using Brewsy.Domain.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Brewsy.Web
{
    public class CheckoutModel : PageModel
    {
        private readonly IChargeService _chargeService;
        private readonly BrewsyContext _brewsyContext;

        public string StripeAccount { get; set; }
        public string SessionId { get; set; }

        public CheckoutModel(BrewsyContext brewsyContext, IChargeService chargeService)
        {
            _chargeService = chargeService;
            _brewsyContext = brewsyContext;
        }

        public void OnGet(int beerId)
        {
            var beer = _brewsyContext.Beers.Include(x => x.User).SingleOrDefault(x => x.Id == beerId);

            StripeAccount = beer.User.StripeUserId;
            SessionId = _chargeService.Charge(beer);
        }
    }
}