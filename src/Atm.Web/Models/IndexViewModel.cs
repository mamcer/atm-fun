using System.ComponentModel.DataAnnotations;

namespace Atm.Web.Models
{
    public class IndexViewModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(3)]
        [RegularExpression("([0-9]+)")]
        public string CardNumber { get; set; }
    }
}