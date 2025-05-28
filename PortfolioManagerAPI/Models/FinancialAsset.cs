using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioManagerAPI.Models
{
    [Table("assets")]
    public class FinancialAsset
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int AssetId { get; set; }
        [Required]
        [MaxLength(48)]
        public string Name { get; set; }

        [MaxLength(48)]
        public string IconFilename { get; set; }
        public string ReferenceIndex { get; set; }
        public string Description { get; set; }
        public string ReferenceETFSite { get; set; }
        public int FavorsExpansion { get; set; }
        public int Defensive { get; set; }
        public int Growth { get; set; }
        public int InflationHedge { get; set; }
        public int Income { get; set; }
        public int Volatility { get; set; }
    }
}
