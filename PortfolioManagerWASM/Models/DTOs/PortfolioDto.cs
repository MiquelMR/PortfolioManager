using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerWASM.Models.DTOs
{
    public class PortfolioDto
    {
        public int PortfolioId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Owner { get; set; }
        public string Description { get; set; }
        public string IconFilename { get; set; }
        public List<PortfolioAssetDto> PortfolioAssetsDto { get; set; } = [];
        public Accessibility Accessibility { get; set; }
    }
}
