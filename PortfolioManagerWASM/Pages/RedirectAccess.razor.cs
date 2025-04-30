using Microsoft.AspNetCore.Components;

namespace PortfolioManagerWASM.Pages
{
    public partial class RedirectAccess
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            NavigationManager.NavigateTo("/login", true);
        }
    }
}
