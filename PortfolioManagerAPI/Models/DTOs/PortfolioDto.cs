using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioManagerAPI.Models
{
    public class PortfolioDto
    {
        [Required]
        [MaxLength(48)]
        public string Name { get; set; }
        [Required]
        public int UserId { get; set; }
        [MaxLength(48)]
        public string Author { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public ICollection<PortfolioAsset> PortfolioAssets { get; set; }
    }
}
