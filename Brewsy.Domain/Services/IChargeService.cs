using Brewsy.Domain.Entities;

namespace Brewsy.Domain.Services
{
    public interface IChargeService
    {
        string Charge(Beer beer);
    }
}
