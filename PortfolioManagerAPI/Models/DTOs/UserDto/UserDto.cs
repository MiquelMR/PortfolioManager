using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioManagerAPI.Models.DTOs.UserDto
{
    public class UserDto
    {
        public string Name { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Email { get; set; }
        public byte[] Avatar { get; set; }
        public UserRole Role { get; set; }
    }
}
