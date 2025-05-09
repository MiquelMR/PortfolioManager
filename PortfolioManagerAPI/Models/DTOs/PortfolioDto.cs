using PortfolioManagerAPI.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerAPI.Models
{
    public class PortfolioDto
    {
        public int PortfolioId { get; set; }
        [Required]
        [MaxLength(48)]
        public string Name { get; set; }
        [MaxLength(48)]
        public string Author { get; set; }
        public string Description { get; set; }
        public string IconFilename { get; set; }
        public List<PortfolioAssetDto> PortfolioAssets { get; set; }
    }
}
