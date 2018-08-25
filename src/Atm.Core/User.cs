using System.Collections.Generic;

namespace Atm.Core
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public int LoginAttemptCount { get; set; }

        public bool IsLocked { get; set; }

        public virtual AtmCard AtmCard { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }

        public User()
        {
            Accounts = new List<Account>();
        }
    }
}