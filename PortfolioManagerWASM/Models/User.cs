using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerWASM.Models
{
    public class User
    {

        [Required]
        [MaxLength(48)]
        public string Name { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(48)]
        public string Email { get; set; }
    }
}
