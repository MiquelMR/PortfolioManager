using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using PortfolioManagerAPI.Helpers;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;
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
        private readonly string resourcePath = config.GetValue<string>("ResourcesPaths:UserAvatar");

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null) { return null; }
            var userDto = _mapper.Map<UserDto>(user);

            if (user.AvatarFilename != null)
            {
                try
                {
                    var portfolioIconFullpath = Path.Combine(resourcePath, user.AvatarFilename);
                    userDto.Avatar = ImageHelper.ImagePathToImage(portfolioIconFullpath);
                }
                catch
                {
                    userDto.Avatar = null;
                }
            }
            return userDto;
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            var users = await _userRepository.GetUsersAsync();
            if (users == null) { return null; }
            var usersDto = _mapper.Map<List<UserDto>>(users);

            return usersDto;
        }

        public async Task<UserDto> UpdateUserAsync(UserUpdateDto userUpdateDto)
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
            if (user == null) { return null; }
            string oldAvatarFilename = user.AvatarFilename;

            _mapper.Map(userUpdateDto, user);

            string avatarFilename = null;
            bool validAvatar = false;
            if (userUpdateDto.Avatar != null)
            {
                try
                {
                    avatarFilename = Guid.NewGuid().ToString() + ImageHelper.GetImageExtension(userUpdateDto.Avatar);
                    validAvatar = true;
                }
                catch
                {
                    avatarFilename = null;
                }
                user.AvatarFilename = avatarFilename;

                var userUpdated = await _userRepository.UpdateUserAsync(user);
                if (userUpdated == null)
                    return null;

                if (validAvatar)
                {
                    var avatarFullpath = Path.Combine(resourcePath, avatarFilename); ;
                    if (ImageHelper.SaveImage(avatarFullpath, userUpdateDto.Avatar))
                    {
                        var oldAvatarFullpath = Path.Combine(resourcePath, oldAvatarFilename ?? "");
                        if (File.Exists(oldAvatarFullpath)) { File.Delete(oldAvatarFullpath); }
                    }
                }
            }
            else
            {
                var success = await _userRepository.UpdateUserAsync(user);
            }

            var userDto = _mapper.Map<UserDto>(userUpdateDto);
            return userDto;
        }

        public async Task<bool> DeleteUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null) { return false; }
            var success = await _userRepository.DeleteUserByEmailAsync(email);
            if (!success) { return false; }

            var avatarFullpath = resourcePath + user.AvatarFilename;
            if (File.Exists(avatarFullpath)) { File.Delete(avatarFullpath); }

            return success;
        }

        public async Task<UserDto> RegisterUserAsync(UserRegisterDto userRegisterDto)
        {
            var (hashedPassword, salt) = PasswordHelper.HashPassword(userRegisterDto.Password);

            var user = _mapper.Map<User>(userRegisterDto);
            user.Password = hashedPassword;
            user.Salt = salt;
            user.RegistrationDate = DateTime.Now;
            user.Role = UserRole.User;

            string avatarFullpath = null;
            string avatarFilename = null;
            if (userRegisterDto.Avatar != null)
            {
                try
                {
                    avatarFilename = Guid.NewGuid().ToString() + ImageHelper.GetImageExtension(userRegisterDto.Avatar);
                    avatarFullpath = Path.Combine(resourcePath, avatarFilename);
                }
                catch
                {
                    avatarFilename = null;
                }
                user.AvatarFilename = avatarFilename;
            }

            var userCreated = await _userRepository.CreateUserAsync(user);
            if (userCreated == null)
                return null;

            if (userRegisterDto.Avatar != null)
            {
                ImageHelper.SaveImage(avatarFullpath, userRegisterDto.Avatar);
            }

            var userDto = _mapper.Map<UserDto>(userRegisterDto);
            return userDto;
        }

        public async Task<UserLoginResponseDto> LoginUserAsync(UserLoginDto userLoginDto)
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
