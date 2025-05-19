using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerWASM.Models
{
    public class AllocationPercentageAttibute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int _portfolioAssetsAggregateAllocation = 0;
            if (value != null)
            {
                var portfolioAssets = (List<PortfolioAsset>)value;
                foreach (PortfolioAsset pa in portfolioAssets)
                {
                    _portfolioAssetsAggregateAllocation += pa.AllocationPercentage;
                }
            }
            return _portfolioAssetsAggregateAllocation == 100
                ? ValidationResult.Success
                : new ValidationResult("Must allocate 100 %");
        }
    }
}
