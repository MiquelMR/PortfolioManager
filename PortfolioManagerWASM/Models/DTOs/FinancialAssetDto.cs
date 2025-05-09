using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerWASM.Models.DTOs
{
    public class FinancialAssetDto
    {
        public int AssetId { get; set; }
        public string Name { get; set; }
        public string IconFilename { get; set; }
    }
}
