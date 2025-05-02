using PortfolioManagerAPI.Models.DTOs.UserDto;

namespace PortfolioManagerAPI.Service.IService
{
    public interface IUserService
    {
        Task<UserDto> GetUserByEmailAsync(string email);
        Task<UserDto> UpdateUserAsync(UserUpdateDto userUpdateDto);
        Task<UserDto> RegisterUserAsync(UserRegisterDto userRegisterDto);
        Task<UserLoginResponseDto> LoginUserAsync(UserLoginDto userLoginDto);
        Task<bool> DeleteUserByEmailAsync(string email);
        Task<bool> ExistsByEmailAsync(string email);
    }
}
