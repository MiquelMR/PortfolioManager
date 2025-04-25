using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerAPI.Models.DTOs
{
    public class PortfolioAssetDto
    {
        public AssetDto Asset { get; set; }
        public float AllocationPercentage { get; set; }
    }
}
