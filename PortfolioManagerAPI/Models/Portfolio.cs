using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerAPI.Models
{
    public class Portfolio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int PortfolioId { get; set; }
        [Required]
        [MaxLength(48)]
        public string Name { get; set; }
        [Required]
        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }
        [MaxLength(48)]
        public string Author{ get; set; }
        public string Description { get; set; }
        public string IconPath { get; set; }
    }
}
