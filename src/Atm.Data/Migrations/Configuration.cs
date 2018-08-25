using System.Linq;
using Atm.Core;

namespace Atm.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<AtmEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AtmEntities context)
        {
            var user = new User
            {
                IsLocked = false,
                LoginAttemptCount = 0,
                UserName = "Mario Moreno"
            };

            var card = new AtmCard
            {
                User = user,
                Number = "123",
                Pin = "1234"
            };

            var account = new Account
            {
                Amount = 10000,
                User = user
            };

            if (!context.Users.Any(u => u.UserName == "Mario Moreno"))
            {
                context.Users.AddOrUpdate(user);
                context.AtmCards.AddOrUpdate(card);
                context.Accounts.AddOrUpdate(account);
            }

            context.SaveChanges();
        }
    }
}