using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerWASM.Models
{
    public class Asset
    {
        public int AssetId { get; set; }
        public string Name { get; set; }
        public byte[] Icon { get; set; }
    }
}
