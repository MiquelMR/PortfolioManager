using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.ViewModels;

namespace PortfolioManagerWASM.Pages
{
    public partial class UserProfile
    {
        [Inject]
        private UserProfileViewModel UserProfileViewModel { get; set; }
        public User ActiveUser { get; set; } = new();
        public string ActiveTab { get; set; }
        public void SelectActiveTab(string ActiveTag)
        {
            ActiveTab = ActiveTag;
        }

        protected override void OnInitialized()
        {
            UserProfileViewModel.Init();
            ActiveUser = UserProfileViewModel.ActiveUser;
        }
    }
}