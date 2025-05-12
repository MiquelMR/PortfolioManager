using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Models.UserDto;
using PortfolioManagerWASM.Services.IService;

namespace PortfolioManagerWASM.ViewModels
{
    public class UserProfileViewModel(IUserService userService)
    {
        private readonly IUserService _userService = userService;

        public User ActiveUser { get; set; } = new();

        public void Init()
        {
            ActiveUser = _userService.ActiveUser;
        }

        public async Task<User> UpdatePublicProfileAsync(UserUpdateDto userUpdateDto)
        {
            userUpdateDto.Email = ActiveUser.Email;
            return await _userService.UpdateUserAsync(userUpdateDto);
        }

        internal async Task<bool> DeleteUserAsync()
        {
            var activeUser = _userService.ActiveUser;
            return await _userService.DeleteUserAsync(activeUser);
        }
    }
}
