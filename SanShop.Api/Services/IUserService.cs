using SanShop.Common.Entities;
using SanShop.Common.Models;

namespace SanShop.Api.Services
{
    public interface IUserService
    {
        LoginResult Login(string userNameOrEmail, string passord);
        RegisterResult Register(string userName, string email, string password);
        User GetUserById(string userId);
    }
}
