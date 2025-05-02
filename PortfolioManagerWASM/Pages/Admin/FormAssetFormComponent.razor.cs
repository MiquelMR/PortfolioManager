using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PortfolioManagerWASM.Models;

namespace PortfolioManagerWASM.Pages.Admin
{
    public partial class FormAssetFormComponent
    {
        [Parameter]
        public Asset FormAsset { get; set; }
        [Parameter]
        public Func<Asset, Task> FormAssetAsyncDelegate { get; set; }
        private async Task FileUploadAsync(InputFileChangeEventArgs e)
        {
            var file = e.File;
            var buffer = new byte[file.Size];

            using var stream = file.OpenReadStream();
            await stream.ReadExactlyAsync(buffer);

            FormAsset.Icon = buffer;
        }
        public async Task UpdateUserAsync()
        {
            if (FormAssetAsyncDelegate != null)
            {
                await FormAssetAsyncDelegate.Invoke(FormAsset);
            }
        }
    }
}
