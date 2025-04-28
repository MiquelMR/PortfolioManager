using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerAPI.Models.DTOs
{
    public class UserLoginDto
    {
        public string Password { get; set; }     
        public string Email { get; set; }
    }
}
