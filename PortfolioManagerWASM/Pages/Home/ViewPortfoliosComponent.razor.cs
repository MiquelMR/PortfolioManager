using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.ViewModels;

namespace PortfolioManagerWASM.Pages.Home
{
    public partial class ViewPortfoliosComponent
    {
        [Parameter]
        public List<Portfolio> UserPortfolios { get; set; }
        [Parameter]
        public Portfolio ActivePortfolio { get; set; }
        [Parameter]
        public Func<int, Task> SelectPortfolioDelegate { get; set; }
        [Parameter]
        public EventCallback<HomeView> ChangeCurrentHomeViewDelegate { get; set; }

        private async Task SelectPortfolio(int index)
        {
            await SelectPortfolioDelegate.Invoke(index);
        }

        private void ChangeCurrentHomeView(HomeView HomeView)
        {
            ChangeCurrentHomeViewDelegate.InvokeAsync(HomeView);
        }
    }
}