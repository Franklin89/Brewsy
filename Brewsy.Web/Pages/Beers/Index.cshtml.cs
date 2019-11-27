using Brewsy.Data;
using Brewsy.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Brewsy.Web
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private BrewsyContext _context;

        public bool HasStripeAccount { get; set; }

        public List<Beer> Beers { get; private set; }

        public IndexModel(BrewsyContext context)
        {
            _context = context;
        }
        
        public void OnGet()
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (!string.IsNullOrEmpty(user.StripeUserId))
            {
                HasStripeAccount = true;
                Beers = _context.Beers.Include(x => x.User).Where(x => x.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToList();
            }
        }
    }
}