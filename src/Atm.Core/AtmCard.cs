namespace Atm.Core
{
    public class AtmCard
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public string Pin { get; set; }

        public virtual User User { get; set; }
    }
}