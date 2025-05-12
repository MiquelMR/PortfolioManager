using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerAPI.Models
{
    public class PortfolioAsset
    {
        public int PortfolioId { get; set; }
        public int AssetId { get; set; }
        [Required]
        [Range(1, 100)]
        public float AllocationPercentage { get; set; }
        [ForeignKey(nameof(PortfolioId))]
        public Portfolio Portfolio { get; set; }

        [ForeignKey(nameof(AssetId))]
        public FinancialAsset FinancialAsset { get; set; }
    }
}
