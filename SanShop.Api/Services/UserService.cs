using SanShop.Common.Entities;
using SanShop.Common.Models;

namespace SanShop.Api.Services
{
    public class UserService : IUserService
    {
        private List<User> _users = new List<User>();

        public User GetUserById(string userId)
        {
            return _users.FirstOrDefault(u => u.Id == userId);
        }

        public User LoginByEmail(string email, string password)
        {
            var user = _users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                bool verifyPassword = BCrypt.Net.BCrypt.Verify(password, user.Password);
                return verifyPassword ? user : null;
            }
            else return null;
        }

        public User LoginByUserName(string userName, string password)
        {
            var user = _users.FirstOrDefault(u => u.UserName == userName);
            if (user != null)
            {
                bool verifyPassword = BCrypt.Net.BCrypt.Verify(password, user.Password);
                return verifyPassword ? user : null;
            }
            else return null;
        }

        public RegisterResult Register(string userName, string email, string password)
        {
            var duplicate = _users.FirstOrDefault(u => u.UserName == userName);
            if (duplicate != null)
                return new RegisterResult { Result = "Podana nazwa Użytkownika już istnieje. Proszę wybrać inną." };

            duplicate = _users.FirstOrDefault(u => u.Email == email);
            if (duplicate != null)
                return new RegisterResult { Result = "Podany email już istnieje. Proszę wybrać inny." };

            User user = new User
            {
                Id = Helper.GetId(),
                UserName = userName,
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(password)
            };

            _users.Add(user);
            return new RegisterResult { User = user, Result = "Rejestracja przebiegła pomyślnie" };
        }
    }
}
