using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerWASM.Models
{
    public class Asset
    {
        public string Name { get; set; }
        public byte[] Icon { get; set; }
    }
}
