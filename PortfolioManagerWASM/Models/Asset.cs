using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerWASM.Models
{
    public class Asset
    {
        [Required]
        [MaxLength(48)]
        public string Name { get; set; }

        [MaxLength(48)]
        public byte[] Icon { get; set; }
    }
}
