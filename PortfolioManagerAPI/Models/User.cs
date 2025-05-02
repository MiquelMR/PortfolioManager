using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioManagerAPI.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int UserId { get; set; }
        [Required]
        [MaxLength(48)]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Salt { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(48)]
        public string Email { get; set; }
        public string AvatarFilename { get; set; }
        [Required]
        public UserRole Role { get; set; }
    }
}
