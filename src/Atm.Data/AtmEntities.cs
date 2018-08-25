using System.Data.Entity;
using Atm.Core;

namespace Atm.Data
{
    public class AtmEntities : DbContext
    {
        public AtmEntities() : base("name=AtmEntities")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }

        public virtual DbSet<AtmCard> AtmCards { get; set; }

        public virtual DbSet<OperationJournal> OperationJournals { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOptional(f => f.AtmCard)
                .WithRequired(s => s.User);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}