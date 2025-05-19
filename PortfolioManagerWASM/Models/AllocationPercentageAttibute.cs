using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerWASM.Models
{
    public class AllocationPercentageAttibute : ValidationAttribute
    {
        private readonly double _value;
    }
}
