using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerWASM.Models.DTOs
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage ="User name is required")]
        public string Password { get; set; }     
        [Required(ErrorMessage ="User password is required")]
        [EmailAddress]
        [MaxLength(48)]
        public string Email { get; set; }
    }
}
