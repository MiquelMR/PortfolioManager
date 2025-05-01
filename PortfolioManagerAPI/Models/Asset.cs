using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioManagerAPI.Models
{
    [Table("assets")]
    public class Asset
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int AssetId { get; set; }

        [Required]
        [MaxLength(48)]
        public string Name { get; set; }

        [MaxLength(48)]
        public string IconPath { get; set; }
    }
}
