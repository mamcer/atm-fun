using System;

namespace Atm.Core
{
    public enum OperationCode
    {
        Balance = 0,
        Withdrawal = 2
    }

    public class OperationJournal
    {
        public int Id { get; set; }

        public virtual AtmCard AtmCard { get; set; }

        public virtual DateTime Date { get; set; }

        public OperationCode OperationCode { get; set; }

        public decimal Amount { get; set; }
    }
}