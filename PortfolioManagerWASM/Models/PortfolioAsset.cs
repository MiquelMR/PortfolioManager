using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerWASM.Models
{
    public class PortfolioAsset
    {
        public FinancialAsset Asset { get; set; }
        public float AllocationPercentage { get; set; }
    }
}
