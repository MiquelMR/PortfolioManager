using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models.DTOs;

namespace PortfolioManagerWASM.Pages.UserProfile
{
    public partial class UpdateViewComponent
    {
        private UserUpdateDto userUpdated = new();
        [Parameter]
        public Func<UserUpdateDto, Task> UpdateUserAsyncDelegate { get; set; }

        public async Task UpdateUserAsync()
        {
            if (UpdateUserAsyncDelegate != null)
            {
                await UpdateUserAsyncDelegate.Invoke(userUpdated);
            }
        }
    }
}