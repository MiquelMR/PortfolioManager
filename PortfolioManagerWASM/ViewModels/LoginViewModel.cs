using PortfolioManagerWASM.Models.UserDto;
using PortfolioManagerWASM.Services.IService;
using PortfolioManagerWASM.Models;

namespace PortfolioManagerWASM.ViewModels
{
    public class LoginViewModel(IUserService userService)
    {
        private readonly IUserService _userService = userService;

        public UserLoginDto UserLoginDto { get; set; } = new UserLoginDto();
        public UserRegisterDto UserRegisterDto { get; set; } = new UserRegisterDto();
        public AuthResponse AuthResponse { get; set; } = new AuthResponse();
        public bool AlreadyLogged { get; set; }

        public void Init()
        {
            AlreadyLogged = _userService.ActiveUser.Email != null;
        }
        public async Task AuthenticateUser(UserLoginDto userLoginDTO)
        {
            var result = await _userService.LoginUserAsync(userLoginDTO);
            AuthResponse.IsSuccess = result.IsSuccess;
        }

        public async Task<User> RegisterUserAsync(UserRegisterDto userRegisterDto)
        {
            return await _userService.RegisterUserAsync(userRegisterDto);
        }
    }
}