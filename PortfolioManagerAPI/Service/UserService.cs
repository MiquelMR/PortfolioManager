using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using PortfolioManagerAPI.Helpers;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;
using PortfolioManagerAPI.Repository.IRepository;
using PortfolioManagerAPI.Service.IService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PortfolioManagerAPI.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly string secretKey;

        public UserService(IUserRepository userRepository, IMapper mapper, IConfiguration config)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            secretKey = config.GetValue<string>("ApiSettings:Secret");
        }

        public async Task<bool> CreateUserAsync(UserRegisterDto userRegisterDto)
        {

            var (hashedPassword, salt) = PasswordHelper.HashPassword(userRegisterDto.Password);
            var user = _mapper.Map<User>(userRegisterDto);
            user.Password = hashedPassword;
            user.Salt = salt;

            return await _userRepository.AddUserAsync(user);
        }

        public async Task<bool> DeleteUserByEmailAsync(string email)
        {
            return await _userRepository.DeleteUserByEmailAsync(email);
        }

        public async Task<bool> DeleteUserByUserIdAsync(int userId)
        {
            return await _userRepository.DeleteUserByIdAsync(userId);
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user =  _userRepository.GetUserByEmailAsync(email);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            var userAppDto = _mapper.Map<UserDto>(user);
            return userAppDto;
        }

        public async Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto)
        {
            var user = _userRepository.GetUserByEmailAsync(userLoginDto.Email);
            if (user == null || !PasswordHelper.VerifyPassword(userLoginDto.Password, user.Salt, user.Password))
            {
                return new UserLoginResponseDto()
                {
                    Token = "",
                    UserLoginDto = null
                };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new(ClaimTypes.Name, user.Email.ToString()),
                ]),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            UserLoginResponseDto userLoginResponseDto = new UserLoginResponseDto()
            {
                Token = tokenHandler.WriteToken(token),
                UserLoginDto = _mapper.Map<UserLoginDto>(user)
            };

            return userLoginResponseDto;
        }

        public async Task<bool> UpdateUserAsync(UserRegisterDto userRegisterDto)
        {
            var user =  _userRepository.GetUserByEmailAsync(userRegisterDto.Email);
            var updatedUser = _mapper.Map<User>(user);
            return await _userRepository.UpdateUserAsync(updatedUser);
        }

        public async Task<bool> UserExistsByEmailAsync(string email)
        {
            return await _userRepository.UserExistsByEmailAsync(email);
        }

        public async Task<bool> UserExistsByIdAsync(int userId)
        {
            return await _userRepository.UserExistsByIdAsync(userId);
        }
    }
}
