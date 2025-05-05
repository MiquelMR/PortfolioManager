using Isopoh.Cryptography.Argon2;
using System.Security.Cryptography;
using System.Text;

namespace PortfolioManagerAPI.Helpers
{
    public static class PasswordHelper
    {
        public static (string hashedPassword, string salt) HashPassword(string password)
        {
            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            string salt = Convert.ToBase64String(saltBytes);
            string saltedPassword = password + salt;
            var hashedPassword = Argon2.Hash(saltedPassword);

            return (hashedPassword, salt);
        }

        public static bool VerifyPassword(string enteredPassword, string storedSalt, string storedHash)
        {
            string saltedPassword = enteredPassword + storedSalt;
            return Argon2.Verify(storedHash, saltedPassword);
        }
    }
}
