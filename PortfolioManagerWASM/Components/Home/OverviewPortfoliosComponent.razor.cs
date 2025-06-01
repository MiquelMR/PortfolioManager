using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Components.Admin;
using PortfolioManagerWASM.Models;
using Syncfusion.Blazor.Charts;
using System.Diagnostics.Metrics;
using System.Drawing;

namespace PortfolioManagerWASM.Components.Home
{
    public partial class OverviewPortfoliosComponent
    {
        // Delegates
        [Parameter] public EventCallback OnDeleteActivePortfolioDelegate { get; set; }
        [Parameter] public EventCallback OnEditActivePortfolioDelegate { get; set; }
        [Parameter] public Func<int, Task> OnSelectPortfolioDelegate { get; set; }

        // Properties
        [Parameter] public Portfolio ActivePortfolio { get; set; }
        [Parameter] public List<Portfolio> UserPortfoliosBasicInfo { get; set; }

        // Private fields
        private List<FinancialAssetsChartDataModel> AssetCompositionChartData { get; set; } = [];
        private List<StrenghtsAndWeaknessChartDataModelPolar> PortfolioStrenghAndWeaknessChartData { get; set; } = [];
        private List<RiskChartDataModelPolar> RiskChartData { get; set; } = [];

        // Events
        private async Task OnDeleteActivePortfolio()
        {
            await OnDeleteActivePortfolioDelegate.InvokeAsync();
            var portfolioDeleted = UserPortfoliosBasicInfo.FirstOrDefault(portfolio => portfolio.PortfolioId == ActivePortfolio.PortfolioId);
            UserPortfoliosBasicInfo.Remove(portfolioDeleted);
            if (UserPortfoliosBasicInfo.Count > 0) await OnSelectPortfolio(0);
        }

        private async Task OnSelectPortfolio(int index)
        {
            await OnSelectPortfolioDelegate.Invoke(index);
            AssetCompositionChartData = PortfolioAssetsChartData();
            PortfolioStrenghAndWeaknessChartData = StrenghtsAndWeakness();
            RiskChartData = Risks();
            StateHasChanged();
        }

        private List<FinancialAssetsChartDataModel> PortfolioAssetsChartData()
        {
            List<FinancialAssetsChartDataModel> chartDataModel = [];
            List<string> colors =
                ["#3498db",
                "#393E46",
                "#F2C078",
                "#f5f5f5",
                "#8be04e",
                "#0d88e6",
                "#7c1158",
                "#00b7c7",
                "#5ad45a"];
            int counter = 0;

            foreach (PortfolioAsset portfolioAsset in ActivePortfolio.PortfolioAssets)
            {
                if (counter > colors.Count - 1)
                    counter = 0;

                var portfolioAssetData = new FinancialAssetsChartDataModel
                {
                    AssetName = portfolioAsset.FinancialAsset.Name,
                    Allocation = portfolioAsset.AllocationPercentage,
                    Color = colors[counter]
                };
                chartDataModel.Add(portfolioAssetData);
                counter++;
            }
            return chartDataModel;
        }

        private List<StrenghtsAndWeaknessChartDataModelPolar> StrenghtsAndWeakness()
        {
            // Establish the characteristics of the portfolio
            float income = 0;
            float inflationHedge = 0;
            float volatility = 0;
            float defensive = 0;
            float expansion = 0;
            float growth = 0;
            foreach (PortfolioAsset portfolioAsset in ActivePortfolio.PortfolioAssets)
            {
                // Provides income
                income +=
                    (portfolioAsset.FinancialAsset.Income - 1) * (portfolioAsset.AllocationPercentage / 100f) * 25;

                // Inflation hedge
                inflationHedge +=
                    (portfolioAsset.FinancialAsset.InflationHedge - 1) * (portfolioAsset.AllocationPercentage / 100f) * 25;

                // Volatility
                volatility +=
                    (portfolioAsset.FinancialAsset.Volatility - 1) * (portfolioAsset.AllocationPercentage / 100f) * 25;

                // Defensive
                defensive +=
                    (portfolioAsset.FinancialAsset.Defensive - 1) * (portfolioAsset.AllocationPercentage / 100f) * 25;

                // Expansión
                expansion +=
                    (portfolioAsset.FinancialAsset.FavorsExpansion - 1) * (portfolioAsset.AllocationPercentage / 100f) * 25;

                // Returns by Growth
                growth +=
                    (portfolioAsset.FinancialAsset.Growth - 1) * (portfolioAsset.AllocationPercentage / 100f) * 25;
            }
            List<StrenghtsAndWeaknessChartDataModelPolar> data =
                [
                    new StrenghtsAndWeaknessChartDataModelPolar() { Environment = "Growth", Intensity= growth, Style=GetTraitStyle(growth), Description = GetDescription("Growth")},
                    new StrenghtsAndWeaknessChartDataModelPolar() { Environment = "Expansion" , Intensity= expansion, Style=GetTraitStyle(expansion), Description = GetDescription("Expansion")},
                    new StrenghtsAndWeaknessChartDataModelPolar() { Environment = "Inflation", Intensity= inflationHedge, Style=GetTraitStyle(inflationHedge), Description = GetDescription("Inflation")},
                    new StrenghtsAndWeaknessChartDataModelPolar() { Environment = "Income", Intensity = income, Style = GetTraitStyle(income), Description = GetDescription("Income")},
                    new StrenghtsAndWeaknessChartDataModelPolar() { Environment = "Defensive", Intensity = defensive ,Style = GetTraitStyle(defensive), Description = GetDescription("Defensive")},
                    new StrenghtsAndWeaknessChartDataModelPolar() { Environment = "Volatiliy", Intensity = volatility, Style = GetTraitStyle(volatility), Description = GetDescription("Volatiliy")},
                 ];
            return data;
        }

        private List<RiskChartDataModelPolar> Risks()
        {
            float politicalRiskIntensity = 0;
            float currencyRiskIntensity = 0;
            float sectorRiskIntensity = 0;
            float ratetRiskIntensity = 0;
            foreach (PortfolioAsset portfolioAsset in ActivePortfolio.PortfolioAssets)
            {
                politicalRiskIntensity += (portfolioAsset.FinancialAsset.PoliticalRisk * portfolioAsset.AllocationPercentage);
                currencyRiskIntensity += (portfolioAsset.FinancialAsset.CurrencyRisk * portfolioAsset.AllocationPercentage);
                sectorRiskIntensity += (portfolioAsset.FinancialAsset.SectorRisk * portfolioAsset.AllocationPercentage);
                ratetRiskIntensity += (portfolioAsset.FinancialAsset.RateRisk * portfolioAsset.AllocationPercentage);
            }

            List<RiskChartDataModelPolar> riskChartData = [
                new RiskChartDataModelPolar()
                {
                    Risk = "Political",
                    Intensity = (int)politicalRiskIntensity,
                    Description = "The potential for investment losses due to political instability or changes in government policy",
                    Color = GetRiskColor((int)currencyRiskIntensity)
                },
                new RiskChartDataModelPolar()
                {
                    Risk="Currency",
                    Intensity = (int)currencyRiskIntensity,
                    Description = "The possibility of loss from fluctuations in exchange rates affecting foreign investments or revenues",
                Color = GetRiskColor((int)currencyRiskIntensity)
                },
                new RiskChartDataModelPolar()
                {
                    Risk="Sector",
                    Intensity = (int)sectorRiskIntensity,
                    Description = "The exposure to adverse performance or conditions within a specific industry or sector",
                    Color = GetRiskColor((int)currencyRiskIntensity)
                },
                new RiskChartDataModelPolar()
                {
                    Risk="Rates",
                    Intensity = (int)ratetRiskIntensity,
                    Description = "The risk of financial impact due to changes in interest rates affecting investment values",
                    Color = GetRiskColor((int)currencyRiskIntensity)
                }
            ];
            return riskChartData;
        }

        private static string GetTraitStyle(float intensity)
        {
            string style;
            if (intensity < 30)
                style = "color:white; font-weight:bold; background-color:var(--blue);";
            else if (intensity < 45)
                style = "color:white; background-color:var(--softerBlue);";
            else if (intensity < 60)
                style = "color:white; background-color:var(--gray);";
            else if (intensity < 70)
                style = "color:white; background-color:var(--softerGold);";
            else
                style = "color:white; font-weight:bold; background-color:var(--gold);";
            return style;
        }

        private static string GetRiskColor(int intensity)
        {
            string color = "#e00c40";
            if (intensity < 30)
                color = "#3498db";
            if (intensity < 60)
                color = "#bdc10a";
            if (intensity < 100)
                color = "#e00c40";
            return color;
        }

        private static string GetDescription(string trait)
        {
            string description;
            switch (trait)
            {
                case "Growth":
                    description = "Potential for capital appreciation through reinvestment of earnings";
                    break;
                case "Expansion":
                    description = "Tendency to perform in line with periods of economic growth and expansion";
                    break;
                case "Inflation":
                    description = "Ability to protect purchasing power by appreciating during inflationary periods";
                    break;
                case "Income":
                    description = "Capability to provide returns in the form of dividends or coupon payments";
                    break;
                case "Defensive":
                    description = "Ability to maintain value or stability during economic downturns";
                    break;
                case "Volatiliy":
                    description = "Susceptibility to frequent or significant price fluctuations";
                    break;
                default:
                    Console.WriteLine("Error providing description");
                    description = "Internal Error";
                    break;
            }
            return description;
        }

        public class FinancialAssetsChartDataModel
        {
            public string AssetName { get; set; }
            public int Allocation { get; set; }
            public string Color { get; set; }
        };

        public class StrenghtsAndWeaknessChartDataModelPolar
        {
            public string Environment { get; set; }
            public float Intensity { get; set; }
            public string Style { get; set; }
            public string Description { get; set; }
        };

        public class RiskChartDataModelPolar
        {
            public string Risk { get; set; }
            public int Intensity { get; set; }
            public string Description { get; set; }
            public string Color { get; set; }
        };
    }
}
