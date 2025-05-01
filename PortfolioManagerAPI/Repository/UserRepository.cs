using Microsoft.EntityFrameworkCore;
using PortfolioManagerAPI.Data;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Repository.IRepository;


namespace PortfolioManagerAPI.Repository
{
    public class UserRepository(ApplicationDbContext db) : IUserRepository
    {
        private readonly ApplicationDbContext _db = db;

        public async Task<User> GetUserByUserIdAsync(int userId)
        {
            try
            {
                return await _db.Users.FirstOrDefaultAsync(user => user.UserId == userId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            try
            {
                return await _db.Users.FirstOrDefaultAsync(user => user.Email == email);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<bool> CreateUserAsync(User user)
        {
            try
            {
                _db.Add(user);
                return await _db.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserByEmailAsync(string email)
        {
            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(user => user.Email == email);
                if (user == null) return false;

                _db.Remove(user);
                return await _db.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                _db.Update(user);
                return await _db.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            try
            {
                return await _db.Users.AnyAsync(u => u.Email == email);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
