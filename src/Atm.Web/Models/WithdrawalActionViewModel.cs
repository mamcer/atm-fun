using System.ComponentModel.DataAnnotations;

namespace Atm.Web.Models
{
    public class WithdrawalActionViewModel
    {
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
    }
}