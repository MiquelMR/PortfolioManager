using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using PortfolioManagerAPI.Helpers;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs.UserDto;
using PortfolioManagerAPI.Repository.IRepository;
using PortfolioManagerAPI.Service.IService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PortfolioManagerAPI.Service
{
    public class UserService(IUserRepository userRepository, IMapper mapper, IConfiguration config) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;
        private readonly string secretKey = config.GetValue<string>("ApiSettings:Secret");

        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            var userDto = _mapper.Map<UserDto>(user);
            if (userDto != null)
            {
                userDto.Avatar = user.AvatarPath != null
                    ? TypeConverter.UserAvatarPathToAvatar(user.AvatarPath)
                    : null;
            }

            return userDto;
        }

        public async Task<UserDto> UpdateAsync(UserUpdateDto userUpdateDto)
        {
            userUpdateDto.GetType().GetProperties()
                .Where(p => p.PropertyType == typeof(string))
                .ToList()
                .ForEach(p =>
                {
                    var value = (string)p.GetValue(userUpdateDto);
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        p.SetValue(userUpdateDto, null);
                    }
                });
            var user = await _userRepository.GetUserByEmailAsync(userUpdateDto.Email);
            _mapper.Map(userUpdateDto, user);
            await _userRepository.UpdateUserAsync(user);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<bool> DeleteByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            var success = await _userRepository.DeleteUserByEmailAsync(email);
            if (success)
            {
                string fullPath = Path.Combine("Resources/UserAvatars/", user.AvatarPath);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
            return success;
        }

        public async Task<bool> RegisterAsync(UserRegisterDto userRegisterDto)
        {

            var (hashedPassword, salt) = PasswordHelper.HashPassword(userRegisterDto.Password);
            var user = _mapper.Map<User>(userRegisterDto);
            user.Password = hashedPassword;
            user.Salt = salt;
            user.RegistrationDate = DateTime.Now;

            // Avatar

            if (userRegisterDto.Avatar != null)
            {
                string filePath = "Resources/UserAvatars/" + userRegisterDto.AvatarFileName;
                //Aqui deberia verse la imagen en el arbol 
                File.WriteAllBytes(filePath, userRegisterDto.Avatar);

            }
            //Aqui deberia guardarse
            user.AvatarPath = userRegisterDto.AvatarFileName;
            return await _userRepository.CreateUserAsync(user);
        }
        public async Task<UserLoginResponseDto> LoginAsync(UserLoginDto userLoginDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(userLoginDto.Email);
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

            UserLoginResponseDto userLoginResponseDto = new()
            {
                Token = tokenHandler.WriteToken(token),
                UserLoginDto = _mapper.Map<UserLoginDto>(user)
            };

            return userLoginResponseDto;
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _userRepository.ExistsByEmailAsync(email);
        }
    }
}
