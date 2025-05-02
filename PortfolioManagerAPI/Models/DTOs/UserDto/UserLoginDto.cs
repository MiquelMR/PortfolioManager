using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerAPI.Models.DTOs.UserDto
{
    public class UserLoginDto
    {
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(48)]
        public string Email { get; set; }
    }
}
