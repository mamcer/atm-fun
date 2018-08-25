using System.Data.Entity;
using System.Linq;
using Atm.Core;

namespace Atm.Data
{
    public class AtmCardRepository : EntityFrameworkRepository<AtmCard, int>, IAtmCardRepository
    {
        public AtmCardRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public AtmCard GetByAtmCardNumber(string atmCardNumber)
        {
            var atmCard = _dbContext.Set<AtmCard>().FirstOrDefault(c => c.Number == atmCardNumber);
            return atmCard;
        }
    }
}
