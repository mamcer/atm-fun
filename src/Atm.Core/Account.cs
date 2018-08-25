namespace Atm.Core
{
    public class Account
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public virtual User User { get; set; }
    }
}