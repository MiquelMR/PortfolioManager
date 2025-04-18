using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PortfolioManagerAPI.Models.DTOs;

namespace PortfolioManagerAPI.Models
{
    public class PortfolioAsset
    {
        public int PortfolioId { get; set; }
        public int AssetId { get; set; }
        [Required]
        public float AllocationPercentage { get; set; }
        [ForeignKey(nameof(PortfolioId))]
        public Portfolio Portfolio { get; set; }

        [ForeignKey(nameof(AssetId))]
        public AssetDto Asset { get; set; }
    }
}
