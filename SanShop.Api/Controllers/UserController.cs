using Microsoft.AspNetCore.Authorization;
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
        public LoginResult Login(LoginModel model)
        {
           return _service.Login(model.UserNameOrEmail, model.Password);
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
        [Authorize]
        public IActionResult GetUserById([FromQuery] string userId)
        {
            var user = _service.GetUserById(userId);
            return user != null ? Ok(user) : BadRequest("Brak użytkownika o podanym id");
        }
    }
}
