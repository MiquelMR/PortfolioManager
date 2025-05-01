using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Models.DTOs;
using PortfolioManagerWASM.Services.IService;

namespace PortfolioManagerWASM.ViewModels
{
    public class UserProfileViewModel(IUserService userService)
    {
        private readonly IUserService _userService = userService;

        public User ActiveUser { get; set; } = new();
        public Func<UserUpdateDto, Task> UpdatePublicProfileAsyncDelegate { get; private set; }
        public Func<Task> DeleteUserAsyncDelegate { get; private set; }

        public void Init()
        {
            ActiveUser = _userService.ActiveUser;
            UpdatePublicProfileAsyncDelegate = UpdatePublicProfileAsync;
            DeleteUserAsyncDelegate = DeleteUserAsync;
        }

        public async Task UpdatePublicProfileAsync(UserUpdateDto userUpdateDto)
        {
            userUpdateDto.Email = ActiveUser.Email;
            await _userService.UpdatePublicProfile(userUpdateDto);
        }

        internal async Task<bool> DeleteUserAsync()
        {
            return await _userService.DeleteUserAsync();
        }
    }
}
