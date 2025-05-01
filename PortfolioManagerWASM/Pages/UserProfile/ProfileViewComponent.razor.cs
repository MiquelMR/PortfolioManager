using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models;

namespace PortfolioManagerWASM.Pages.UserProfile
{
    public partial class ProfileViewComponent
    {
        [Parameter]
        public User ActiveUser { get; set; }
    }
}