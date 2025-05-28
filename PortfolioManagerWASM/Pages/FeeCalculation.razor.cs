namespace PortfolioManagerWASM.Pages
{
    public partial class FeeCalculation
    {
        private FeeStructure feeStructure = new();

        private void OnCalculateFees()
        {
            feeStructure.CalculateFeesAndTaxes();
        }
    }

    public class FeeStructure
    {
        public FeeStructure()
        {

        }

        public int quantityBought;
        public int yearlyPurchases;
        public int yearsInvesting;

        public int purcharseComission = 0;
        public float brokerageFee = 0;

        public int dividendTaxes = 0;
        public int earningTaxes = 0;

        public int totalInvested = 0;
        public int totalReturns = 0;
        public int totalBrokerageFees = 0;
        public int totalCapital = 0;

        private readonly float yield = 0.0672f;
        private readonly float dividendYield = 0.04f;
        private readonly float dividendTax = 0.023f;
        private readonly float earningTax = 0.023f;

        public int CalculateFeesAndTaxes()
        {
            for (int i = 0; i < yearsInvesting; i++)
            {
                totalReturns += (int)(totalCapital * yield);
                totalInvested += quantityBought * yearlyPurchases;
                totalCapital = totalReturns + totalInvested;
                purcharseComission += yearlyPurchases * purcharseComission;
                totalBrokerageFees += (int)(totalInvested * brokerageFee / 100);
                dividendTaxes += (int)(totalInvested * dividendYield * dividendTax);
                earningTaxes += (int)(totalInvested * earningTax);
            }

            return totalInvested;
        }


    }
}