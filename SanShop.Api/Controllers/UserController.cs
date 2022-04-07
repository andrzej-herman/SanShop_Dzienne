using Microsoft.AspNetCore.Mvc;
using SanShop.Api.Services;
using SanShop.Common.Entities;
using SanShop.Common.Models;

namespace SanShop.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginModel model)
        {
            User user = null;
            if (model.UserNameOrEmail.Contains("@"))
                user = _service.LoginByEmail(model.UserNameOrEmail, model.Password);
            else
                user = _service.LoginByUserName(model.UserNameOrEmail, model.Password);

            return user != null ? Ok(user) : BadRequest("Nieprawidłowy email lub hasło.");
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterModel model)
        {
            var result = _service.Register(model.UserName, model.Email, model.Password);
            return result.User != null ? Ok(result) : BadRequest(result.Result);
        }

        [HttpGet]
        [Route("user")]
        public IActionResult GetUserById([FromQuery] string userId)
        {
            var user = _service.GetUserById(userId);
            return user != null ? Ok(user) : BadRequest("Brak użytkownika o podanym id");
        }
    }
}
