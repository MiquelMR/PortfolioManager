using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PortfolioManagerAPI.Data;
using PortfolioManagerAPI.Helpers;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;
using PortfolioManagerAPI.Repository.IRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;

namespace PortfolioManagerAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly string secretKey;

        public UserRepository(ApplicationDbContext db, IConfiguration config)
        {
            _db = db;
            secretKey = config.GetValue<string>("ApiSettings:Secret");
        }

        public User GetUserById(int UserId)
        {
            return _db.Users.FirstOrDefault(user => user.UserId == UserId);
        }

        public bool UserAlreadyExistsByEmail(string Email)
        {
            var user = _db.Users.FirstOrDefault(user => user.Email == Email);
            return user != null;
        }

        public async Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == userLoginDto.Email.ToLower());

            if (user == null || !PasswordHelper.VerifyPassword(userLoginDto.Password, user.Salt, user.Password))
            {
                return new UserLoginResponseDto()
                {
                    Token = "",
                    User = null
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
                User = user
            };

            return userLoginResponseDto;
        }

        public async Task<User> Register(UserRegisterDto userRegisterDto)
        {
            var (hashedPassword, salt) = PasswordHelper.HashPassword(userRegisterDto.Password);

            User user = new()
            {
                Name = userRegisterDto.Name,
                Email = userRegisterDto.Email,
                Password = hashedPassword,
                Salt = salt,
                RegistrationDate = DateTime.Now
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }
    }
}
