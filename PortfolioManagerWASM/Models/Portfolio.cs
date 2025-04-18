using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerWASM.Models
{
    public class Portfolio
    {
        [Required]
        [MaxLength(48)]
        public string Name { get; set; }
        [MaxLength(48)]
        public string Author { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
    }
}
