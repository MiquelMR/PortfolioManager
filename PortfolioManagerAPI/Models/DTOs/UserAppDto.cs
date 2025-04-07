using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioManagerAPI.Models
{
    public class UserAppDto
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
