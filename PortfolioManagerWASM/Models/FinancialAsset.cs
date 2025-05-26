using PortfolioManagerWASM.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerWASM.Models
{
    public class FinancialAsset
    {
        public int AssetId { get; set; }
        public string Name { get; set; }
        public string IconPath { get; set; }
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
