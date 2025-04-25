using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerWASM.Models
{
    public class Portfolio
    {
        public int PortfolioId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public byte[] Icon { get; set; }
        public List<PortfolioAsset> PortfolioAssets { get; set; }
    }
}
