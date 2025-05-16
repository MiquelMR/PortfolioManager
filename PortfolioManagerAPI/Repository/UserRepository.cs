using Microsoft.EntityFrameworkCore;
using PortfolioManagerAPI.Data;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Repository.IRepository;


namespace PortfolioManagerAPI.Repository
{
    public class UserRepository(ApplicationDbContext db) : IUserRepository
    {
        private readonly ApplicationDbContext _db = db;

        public async Task<User> GetUserByEmailAsync(string email)
        {
            try
            {
                return await _db.Users.FirstOrDefaultAsync(user => user.Email == email);
            }
            catch
            {
                return null;
            }
        }

        public async Task<int> GetUserIdByNameAsync(string name)
        {
            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(user => user.Name == name);
                return user.UserId;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                return await _db.Users.OrderBy(user => user.Name).ToListAsync();
            }
            catch
            {
                return null;
            }
        }
        public async Task<User> CreateUserAsync(User user)
        {
            try
            {
                var userCreated = await _db.AddAsync(user);
                var success = await _db.SaveChangesAsync() > 0;
                if (success)
                    return userCreated.Entity;
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            try
            {
                var userUpdated = _db.Update(user);
                var success = await _db.SaveChangesAsync() > 0;
                if (success)
                    return userUpdated.Entity;
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteUserByEmailAsync(string email)
        {
            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(user => user.Email == email);
                if (user == null)
                    return false;

                _db.Remove(user);
                var success = await _db.SaveChangesAsync() > 0;
                return success;
            }
            catch
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
            catch
            {
                return false;
            }
        }
    }
}
