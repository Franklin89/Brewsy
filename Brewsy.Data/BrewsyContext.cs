using Brewsy.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Brewsy.Data
{
    public class BrewsyContext : IdentityDbContext<User>
    {
        public BrewsyContext(DbContextOptions<BrewsyContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Beer> Beers { get; set; }
    }
}
