using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PortfolioManagerWASM.Models.UserDto;

namespace PortfolioManagerWASM.Components.Login
{
    public partial class SignInFormComponent
    {
        [Parameter]
        public Func<UserRegisterDto, Task> RegisterNewUserAsyncDelegate { get; set; }

        private readonly UserRegisterDto newUser = new();

        public async Task RegisterNewUserAsync()
        {
            if (RegisterNewUserAsyncDelegate != null)
            {
                await RegisterNewUserAsyncDelegate.Invoke(newUser);
            }
        }

        private async Task FileUploadAsync(InputFileChangeEventArgs e)
        {
            var file = e.File;
            var buffer = new byte[file.Size];

            using var stream = file.OpenReadStream();
            await stream.ReadExactlyAsync(buffer);

            newUser.Avatar = buffer;
        }
    }
}