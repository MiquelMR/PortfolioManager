using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerAPI.Models.DTOs
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "User name is required")]
        [MaxLength(48)]
        public string Name { get; set; }
        [Required(ErrorMessage = "User password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email password is required")]
        [EmailAddress]
        [MaxLength(48)]
        public string Email { get; set; }
    }
}
