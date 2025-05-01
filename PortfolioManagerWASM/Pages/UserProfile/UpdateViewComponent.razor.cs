using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models.DTOs;

namespace PortfolioManagerWASM.Pages.UserProfile
{
    public partial class UpdateViewComponent
    {
        private readonly UserUpdateDto publicProfileUpdated = new();
        [Parameter]
        public Func<UserUpdateDto, Task> UpdatePublicProfileAsyncDelegate { get; set; }

        public async Task UpdateUserAsync()
        {
            if (UpdatePublicProfileAsyncDelegate != null)
            {
                await UpdatePublicProfileAsyncDelegate.Invoke(publicProfileUpdated);
            }
        }
    }
}