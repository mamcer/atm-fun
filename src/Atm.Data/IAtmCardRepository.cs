using Atm.Core;

namespace Atm.Data
{
    public interface IAtmCardRepository : IRepository<AtmCard, int>
    {
        AtmCard GetByAtmCardNumber(string atmCardNumber);
    }
}