using SanShop.Common.Entities;
using SanShop.Common.Models;

namespace SanShop.Api.Services
{
    public interface IUserService
    {
        User LoginByEmail(string email, string passord);
        User LoginByUserName(string userName, string passord);
        RegisterResult Register(string userName, string email, string password);
        User GetUserById(string userId);
    }
}
