using System;
using System.ComponentModel.DataAnnotations;

namespace Atm.Web.Models
{
    public class BalanceViewModel
    {
        [DataType(DataType.CreditCard)]
        public string CardNumber { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
    }
}