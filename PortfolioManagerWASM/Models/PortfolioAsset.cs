using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerWASM.Models
{
    public class PortfolioAsset
    {
        public FinancialAsset FinancialAsset { get; set; }
        public int AllocationPercentage { get; set; }
    }
}
