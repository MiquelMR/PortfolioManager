using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerAPI.Models.DTOs.UserDto
{
    public class UserLoginDto
    {
        public string Password { get; set; }     
        public string Email { get; set; }
    }
}
