using Microsoft.IdentityModel.Tokens;
using SanShop.Common.Entities;
using SanShop.Common.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SanShop.Api.Services
{
    public class UserService : IUserService
    {
        private List<User> _users = new List<User>();

        public User GetUserById(string userId)
        {
            return _users.FirstOrDefault(u => u.Id == userId);
        }

        public LoginResult Login(string userNameOrEmail, string password)
        {
            if (string.IsNullOrEmpty(userNameOrEmail)) 
                return new LoginResult() { Result = false, Description = "Nieprawidłowe dane logowania" };

            User user;
            if (userNameOrEmail.Contains("@"))
                user = _users.FirstOrDefault(u => u.Email == userNameOrEmail);
            else
                user = _users.FirstOrDefault(u => u.UserName == userNameOrEmail);

            if (user != null)
            {
                bool verifyPassword = BCrypt.Net.BCrypt.Verify(password, user.Password);
                if (!verifyPassword)
                    return new LoginResult() { Result = false, Description = "Nieprawidłowe dane logowania" };

                var token = GenerateToken(user);
                return new LoginResult() { Result = true, Description = "Logowanie udane", Token = token };
            }
            else
                return new LoginResult() { Result = false, Description = "Nieprawidłowe dane logowania" };
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

        private string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Sid, user.Id),
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.Email, user.Email),
            };

            var tokenKey = "136e5ce145143ae87!9d1656f2&468cc2bae376e#6aad54Hbf1b1cac6955674e59b";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
