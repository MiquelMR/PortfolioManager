using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerWASM.Models.DTOs
{
    public class PortfolioAssetDto
    {
        public FinancialAssetDto FinancialAssetDto { get; set; }
        public float AllocationPercentage { get; set; }
    }
}
