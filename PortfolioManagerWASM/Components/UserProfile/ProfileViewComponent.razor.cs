using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models;

namespace PortfolioManagerWASM.Components.UserProfile
{
    public partial class ProfileViewComponent
    {
        [Parameter]
        public User ActiveUser { get; set; }
    }
}