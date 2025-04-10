using PortfolioManagerAPI.Models.DTOs;
using PortfolioManagerAPI.Models;

namespace PortfolioManagerAPI.Service.IService
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(int userId);
        Task<UserDto> GetUserByEmailAsync(string email);
        Task<bool> CreateUserAsync(UserRegisterDto userRegisterDto);
        Task<bool> UpdateUserAsync(UserRegisterDto userRegisterDto);
        Task<bool> DeleteUserByUserIdAsync(int userId);
        Task<bool> DeleteUserByEmailAsync(string email);
        Task<bool> UserExistsByIdAsync(int userId);
        Task<bool> UserExistsByEmailAsync(string email);
        Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto);
    }
}
