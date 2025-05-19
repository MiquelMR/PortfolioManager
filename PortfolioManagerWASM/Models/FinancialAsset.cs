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
    }
}
