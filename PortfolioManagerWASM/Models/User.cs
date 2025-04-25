using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioManagerWASM.Models
{
    [Table("users")]
    public class User
    {
        public string Name { get; set; }
        [Required]
        public DateTime? RegistrationDate { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(48)]
        public string Email { get; set; }
    }
}
