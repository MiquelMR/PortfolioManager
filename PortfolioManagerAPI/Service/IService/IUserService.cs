using PortfolioManagerAPI.Models.DTOs;
using PortfolioManagerAPI.Models;

namespace PortfolioManagerAPI.Service.IService
{
    public interface IUserService
    {
        Task<bool> Register(UserRegisterDto userRegisterDto);
        Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto);
        Task<bool> UpdateAsync(UserRegisterDto userRegisterDto);
        Task<bool> DeleteByEmailAsync(string email);
        Task<UserDto> GetByEmailAsync(string email);
        Task<bool> ExistsByEmailAsync(string email);
    }
}
