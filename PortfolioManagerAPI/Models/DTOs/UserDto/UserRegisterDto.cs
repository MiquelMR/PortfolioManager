using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerAPI.Models.DTOs.UserDto
{
    public class UserRegisterDto
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public byte[] Avatar { get; set; }
    }
}
