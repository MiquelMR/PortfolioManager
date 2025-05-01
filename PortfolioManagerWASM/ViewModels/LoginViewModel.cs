using PortfolioManagerWASM.Models.DTOs;
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
        public Func<UserLoginDto, Task> LoginUserAsyncDelegate { get; private set; }
        public Func<UserRegisterDto, Task> RegisterUserAsyncDelegate { get; private set; }
        public bool AlreadyLogged { get; set; }

        public void Init()
        {
            LoginUserAsyncDelegate = AuthenticateUser;
            RegisterUserAsyncDelegate = RegisterUserAsync;
            AlreadyLogged = _userService.ActiveUser.Email != null;
        }
        public async Task AuthenticateUser(UserLoginDto userLoginDTO)
        {
            var result = await _userService.LoginUser(userLoginDTO);
            AuthResponse.IsSuccess = result.IsSuccess;
        }

        public async Task RegisterUserAsync(UserRegisterDto userRegisterDto)
        {
            await _userService.RegisterUser(userRegisterDto);
        }
    }
}