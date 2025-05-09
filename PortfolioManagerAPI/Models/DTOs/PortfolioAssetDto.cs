using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerAPI.Models.DTOs
{
    public class PortfolioAssetDto
    {
        [Required]
        public FinancialAssetDto Asset { get; set; }
        [Required]
        [Range(1, 100)]
        public float AllocationPercentage { get; set; }
    }
}
