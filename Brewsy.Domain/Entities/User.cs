using Microsoft.AspNetCore.Identity;

namespace Brewsy.Domain.Entities
{
    public class User : IdentityUser
    {
        public string StripeUserId { get; set; }

    }
}
