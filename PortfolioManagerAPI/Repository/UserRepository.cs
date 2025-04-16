using Microsoft.EntityFrameworkCore;
using PortfolioManagerAPI.Data;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Repository.IRepository;


namespace PortfolioManagerAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(ApplicationDbContext db, ILogger<UserRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(User user)
        {
            try
            {
                _db.Add(user);
                return await _db.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error creating user");
                return false;
            }
        }

        public async Task<bool> DeleteByEmailAsync(string email)
        {
            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(user => user.Email == email);
                if (user == null) return false;

                _db.Remove(user);
                return await _db.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user");
                return false;
            }
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            try
            {
                return await _db.Users.FirstOrDefaultAsync(user => user.Email == email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user");
                return null;
            }
        }

        public async Task<bool> UpdateAsync(User user)
        {
            try
            {
                _db.Update(user);
                return await _db.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user");
                return false;
            }
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            try
            {
                return await _db.Users.AnyAsync(u => u.Email == email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking user");
                return false;
            }
        }
    }
}
