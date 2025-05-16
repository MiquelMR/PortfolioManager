using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PortfolioManagerWASM.Models.UserDto;

namespace PortfolioManagerWASM.Components.UserProfile
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
        private async Task FileUploadAsync(InputFileChangeEventArgs e)
        {
            var file = e.File;
            var buffer = new byte[file.Size];

            using var stream = file.OpenReadStream();
            await stream.ReadExactlyAsync(buffer);

            publicProfileUpdated.Avatar = buffer;
        }
    }
}