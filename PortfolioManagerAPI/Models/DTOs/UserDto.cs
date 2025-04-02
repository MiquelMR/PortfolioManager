using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerAPI.Models.DTOs
{
    public class UserDto
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
