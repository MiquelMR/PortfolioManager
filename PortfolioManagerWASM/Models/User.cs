using PortfolioManagerAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioManagerWASM.Models
{
    [Table("users")]
    public class User
    {
        public string Name { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string Email { get; set; }
        public byte[] Avatar { get; set; }
        public UserRoles Role { get; set; }
    }
}
