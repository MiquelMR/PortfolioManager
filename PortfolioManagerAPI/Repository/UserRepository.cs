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

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            _db.Users.Add(user);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUserByEmailAsync(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
            _db.Users.Remove(user);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUserByIdAsync(int userId)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            _db.Users.Remove(user);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _db.Users.Update(user);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UserExistsByEmailAsync(string email)
        {
            return await _db.Users.AnyAsync(x => x.Email == email);
        }

        public async Task<bool> UserExistsByIdAsync(int userId)
        {
            return await _db.Users.AnyAsync(x => x.UserId == userId);
        }
    }
}
