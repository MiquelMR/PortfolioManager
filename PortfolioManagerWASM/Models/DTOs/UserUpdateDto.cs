using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerWASM.Models.DTOs
{
    public class UserUpdateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] Avatar { get; set; }
    }
}
