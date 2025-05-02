using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioManagerAPI.Models.DTOs.UserDto
{
    public class UserDto
    {
        [Required]
        [MaxLength(48)]
        public string Name { get; set; }
        public DateTime RegistrationDate { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(48)]
        public string Email { get; set; }
        public byte[] Avatar { get; set; }
        [Required]
        public UserRole Role { get; set; }
    }
}
