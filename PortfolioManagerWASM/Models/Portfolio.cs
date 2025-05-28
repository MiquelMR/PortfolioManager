using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerWASM.Models
{
    public class Portfolio
    {
        public int PortfolioId { get; set; }
        [Required(ErrorMessage = "Name field can't be empty")]
        public string Name { get; set; }
        public string Author { get; set; }
        public string Owner { get; set; }
        public string Description { get; set; }
        public string IconPath { get; set; }
        [AllocationPercentageAttibute]
        public List<PortfolioAsset> PortfolioAssets { get; set; } = [];
        public Accessibility Accessibility { get; set; }
    }
}
