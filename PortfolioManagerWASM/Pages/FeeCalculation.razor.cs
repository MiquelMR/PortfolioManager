namespace PortfolioManagerWASM.Pages
{
    public partial class FeeCalculation
    {
        public readonly FeeStructure feeStructure = new();

        private void OnCalculateFees()
        {
            feeStructure.ResetValues();
            feeStructure.CalculateFeesAndTaxes();
        }
    }

    public class FeeStructure
    {
        // Data for the charts
        public List<FeeChartData> purcharseComisionsDataChart = [];
        public List<FeeChartData> custodyComisionsDataChart = [];
        public List<FeeChartData> brokerageFeesDataChart = [];
        public List<FeeChartData> taxesOnDividendsDataChart = [];
        public List<FeeChartData> taxesOnBenefitsDataChart = [];
        public List<FeeChartData> dividendsDataChart = [];
        public List<FeeChartData> benefitsDataChart = [];

        public int accumulatedCapital = 0;
        public int accumulatedReturnsAsCapital = 0;
        public int accumulatedReturnsAsDividends = 0;
        public int accumulatedPurcharseComissions = 0;
        public int accumulatedCustodyComissions = 0;
        public int accumulatedBrokerageFees = 0;
        public int accumulatedTaxesOnBenefit = 0;
        public int accumulatedTaxesOnDividends = 0;

        public int quantityBought = 0;
        public int yearlyPurchases = 0;
        public int yearsInvesting = 0;
        public int purcharseCommission = 0;
        public float custodyCommission = 0;
        public float brokerageFee = 0;
        public int dividendTaxes = 0;
        public int earningTaxes = 0;
        public int accumulatedInvestment = 0;
        public float dividendsAsPercentageOfReturns = 0;
        public float expectedYield = 10f;
        public float dividendTax = 20f;
        public float earningTax = 20f;

        public int graphMaxTimeLenght = 0;
        public void CalculateFeesAndTaxes()
        {
            for (int i = 0; i <= yearsInvesting; i++)
            {
                // Pay commissions and fees
                var yearPurcharseCommission = yearlyPurchases * purcharseCommission;
                var yearCustodyCommission = (accumulatedCapital * custodyCommission / 100);
                var yearBrokerageFees = (accumulatedCapital * brokerageFee / 100);
                var totalCommissions = yearPurcharseCommission + yearCustodyCommission + yearBrokerageFees;

                // Calculate yields on the previous year minus dividends
                var yearGrossYieldAsCapital = (accumulatedCapital * (1 - dividendsAsPercentageOfReturns / 100) * expectedYield / 100);
                var yearGrossYieldAsDividends = (accumulatedCapital * dividendsAsPercentageOfReturns / 100 * expectedYield / 100);
                var yearTotalInvested = quantityBought * yearlyPurchases;
                // Gross after commissions
                var yearGrossYieldAsCapitalAfterCommissions = yearGrossYieldAsCapital - totalCommissions;

                // Now we pay taxes
                var yearTaxesOnDividends = (int)(yearGrossYieldAsDividends * dividendTax / 100);
                // You don't pay until you sell so last year
                var yearTaxesOnCapital = yearsInvesting == i
                    ? (int)(accumulatedReturnsAsCapital * earningTax / 100)
                    : 0;

                // Net yields
                var yearNetYieldAsDividends = yearGrossYieldAsDividends - yearTaxesOnDividends;
                var yearNetYieldAsCapital = yearGrossYieldAsCapitalAfterCommissions - yearTaxesOnCapital;

                // Update the accumulate values for the graphs
                // Update Fees and commissions
                accumulatedPurcharseComissions += yearPurcharseCommission;
                accumulatedCustodyComissions += (int)yearCustodyCommission;
                accumulatedBrokerageFees += (int)yearBrokerageFees;
                // Update Capital yields
                accumulatedReturnsAsCapital += (int)yearNetYieldAsCapital;
                accumulatedReturnsAsDividends += (int)yearNetYieldAsDividends;
                // Update taxes
                accumulatedTaxesOnDividends += yearTaxesOnDividends;
                accumulatedTaxesOnBenefit += yearTaxesOnCapital;
                // Update after taxes
                accumulatedReturnsAsDividends += (int)yearNetYieldAsDividends;
                accumulatedReturnsAsCapital += (int)yearNetYieldAsCapital;
                // Update capital
                accumulatedCapital += (int)(yearNetYieldAsCapital + yearTotalInvested);
                accumulatedInvestment += yearTotalInvested;

                purcharseComisionsDataChart.Add(new FeeChartData
                {
                    Year = i,
                    Amount = accumulatedPurcharseComissions
                });
                custodyComisionsDataChart.Add(new FeeChartData
                {
                    Year = i,
                    Amount = accumulatedCustodyComissions
                });
                brokerageFeesDataChart.Add(new FeeChartData
                {
                    Year = i,
                    Amount = accumulatedBrokerageFees
                });
                taxesOnDividendsDataChart.Add(new FeeChartData
                {
                    Year = i,
                    Amount = accumulatedTaxesOnDividends
                });
                dividendsDataChart.Add(new FeeChartData
                {
                    Year = i,
                    Amount = accumulatedReturnsAsDividends
                });
                benefitsDataChart.Add(new FeeChartData
                {
                    Year = i,
                    Amount = accumulatedReturnsAsCapital
                });
                taxesOnBenefitsDataChart.Add(new FeeChartData
                {
                    Year = i,
                    Amount = accumulatedTaxesOnBenefit
                });
            }
        }
        public void ResetValues()
        {
            accumulatedCapital = 0;
            accumulatedPurcharseComissions = 0;
            accumulatedCustodyComissions = 0;
            accumulatedBrokerageFees = 0;
            accumulatedTaxesOnBenefit = 0;
            accumulatedTaxesOnDividends = 0;
            accumulatedReturnsAsCapital = 0;
            accumulatedReturnsAsDividends = 0;
            accumulatedInvestment = 0;

            purcharseComisionsDataChart = [];
            custodyComisionsDataChart = [];
            brokerageFeesDataChart = [];
            taxesOnDividendsDataChart = [];
            taxesOnBenefitsDataChart = [];
            dividendsDataChart = [];
            benefitsDataChart = [];
        }
    }

    public class FeeChartData
    {
        public int Year { get; set; }
        public int Amount { get; set; }
    }
}