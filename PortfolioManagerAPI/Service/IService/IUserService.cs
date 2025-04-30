using PortfolioManagerAPI.Models.DTOs.UserDto;

namespace PortfolioManagerAPI.Service.IService
{
    public interface IUserService
    {
        Task<UserDto> GetByEmailAsync(string email);
        Task<bool> UpdateAsync(UserUpdateDto userUpdateDto);
        Task<bool> DeleteByEmailAsync(string email);
        Task<bool> RegisterAsync(UserRegisterDto userRegisterDto);
        Task<UserLoginResponseDto> LoginAsync(UserLoginDto userLoginDto);
        Task<bool> ExistsByEmailAsync(string email);
    }
}
