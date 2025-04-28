using PortfolioManagerAPI.Models.DTOs;

namespace PortfolioManagerAPI.Models
{
    public class PortfolioDto
    {
        public int PortfolioId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public byte[] Icon { get; set; }
        public ICollection<PortfolioAssetDto> PortfolioAssets { get; set; }
    }
}
