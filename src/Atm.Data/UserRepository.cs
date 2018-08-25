using System.Data.Entity;
using Atm.Core;

namespace Atm.Data
{
    public class AccountRepository : EntityFrameworkRepository<Account, int>, IAccountRepository
    {
        public AccountRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
