using System.ComponentModel.DataAnnotations;

namespace Atm.Web.Models
{
    public class PinViewModel
    {
        [Required]
        public string CardNumber { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(4)]
        [DataType(DataType.Password)]
        public string Pin { get; set; }
    }
}