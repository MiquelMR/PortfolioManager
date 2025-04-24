using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Services;
using PortfolioManagerWASM.Services.IService;

namespace PortfolioManagerWASM.Components
{
    public partial class Navbar
    {
        private readonly IAppService _appService;
        public User ActiveUser { get; set; }

        public Navbar(IAppService AppService)
        {
            _appService = AppService;
        }

        protected override async Task OnInitializedAsync()
        {
            ActiveUser = _appService.UserService.ActiveUser;

            if (ActiveUser == null)
            {
                Console.WriteLine("ActiveUser is not initialized.");
            }
        }

    }
}