using PortfolioManagerAPI.Data;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Repository.IRepository;

namespace PortfolioManagerAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateUser(User user)
        {
            user.RegistrationDate = DateTime.Now;
            _db.Users.Add(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _db.Users.Remove(user);
            return Save();
        }

        public bool ExistsById(int UserId)
        {
            bool result = _db.Users.Any(user => user.UserId == UserId);
            return result;
        }

        public User GetUserById(int UserId)
        {
            return _db.Users.FirstOrDefault(user => user.UserId == UserId);
        }
        public ICollection<User> GetUsers()
        {
            return _db.Users.OrderBy(user => user.UserId).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        public bool UpdateUser(User user)
        {
            _db.Users.Update(user);
            return Save();
        }
    }
}
