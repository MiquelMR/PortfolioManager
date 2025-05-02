using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerWASM.Models.UserDto
{
    public class UserDeleteDto
    { 
        [Required(ErrorMessage ="User password is required")]
        [EmailAddress]
        [MaxLength(48)]
        public string Email { get; set; }
    }
}
