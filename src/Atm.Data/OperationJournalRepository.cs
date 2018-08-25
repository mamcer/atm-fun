using System.Data.Entity;
using Atm.Core;

namespace Atm.Data
{
    public class OperationJournalRepository : EntityFrameworkRepository<OperationJournal, int>, IOperationJournalRepository
    {
        public OperationJournalRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
