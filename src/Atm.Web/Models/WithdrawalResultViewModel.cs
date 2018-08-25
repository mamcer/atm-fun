using System;
using System.ComponentModel.DataAnnotations;

namespace Atm.Web.Models
{
    public class WithdrawalResultViewModel
    {
        [DataType(DataType.CreditCard)]
        public string CardNumber { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [DataType(DataType.Currency)]
        public decimal AmountWithdraw { get; set; }

        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }
    }
}