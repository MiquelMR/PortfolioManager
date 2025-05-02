using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerAPI.Models.DTOs.UserDto
{
    public class UserRegisterDto
    {
        [Required]
        [MaxLength(48)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(48)]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(48)]
        public string Email { get; set; }
        public byte[] Avatar { get; set; }
    }
}
