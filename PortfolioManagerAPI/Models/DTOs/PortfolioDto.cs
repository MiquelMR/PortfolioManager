using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerAPI.Models
{
    public class PortfolioDto
    {
        [Required]
        [MaxLength(48)]
        public string Name { get; set; }
        [MaxLength(48)]
        public string Author{ get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
    }
}
