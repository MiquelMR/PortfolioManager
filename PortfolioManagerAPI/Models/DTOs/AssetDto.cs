using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioManagerAPI.Models.DTOs{

    public class AssetDto
    {
        [Required]
        [MaxLength(48)]
        public string Name { get; set; }

        [MaxLength(48)]
        public byte[] Icon { get; set; }
    }
}
