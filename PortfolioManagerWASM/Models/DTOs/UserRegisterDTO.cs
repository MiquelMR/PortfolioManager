using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerWASM.Models.DTOs
{
    public class UserRegisterDTO
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
