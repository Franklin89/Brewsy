using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Brewsy.Data
{
    public class BrewsyContextFactory : IDesignTimeDbContextFactory<BrewsyContext>
    {
        public BrewsyContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BrewsyContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Brewsy;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new BrewsyContext(optionsBuilder.Options);
        }
    }
}
