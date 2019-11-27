using System;

namespace Brewsy.Domain.Entities
{
    public class Purchase
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string StripePaymentId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
