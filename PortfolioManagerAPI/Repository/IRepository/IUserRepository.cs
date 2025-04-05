using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;

namespace PortfolioManagerAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        User GetUserById(int UserId);
        bool UserAlreadyExistsByEmail(string Email);
        Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto);
        Task<User> Register(UserRegisterDto userRegisterDto);
    }
}
