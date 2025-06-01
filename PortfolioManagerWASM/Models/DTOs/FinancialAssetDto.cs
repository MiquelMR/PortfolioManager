using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerWASM.Models.DTOs
{
    public class FinancialAssetDto
    {
        public int AssetId { get; set; }
        public string Name { get; set; }
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
        public int CurrencyRisk { get; set; }
        public int SectorRisk { get; set; }
        public int PoliticalRisk { get; set; }
        public int RateRisk { get; set; }
    }
}
