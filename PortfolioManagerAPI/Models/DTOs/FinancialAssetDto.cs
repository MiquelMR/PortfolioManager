using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerAPI.Models.DTOs
{
    public class FinancialAssetDto
    {
        public int AssetId { get; set; }
        [Required]
        [MaxLength(48)]
        public string Name { get; set; }
        public string IconFilename { get; set; }
        public string ReferenceIndex { get; set; }
        public string Description { get; set; }
    }
}
