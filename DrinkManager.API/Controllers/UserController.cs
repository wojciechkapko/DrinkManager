using BLL.Contracts.Requests;
using BLL.Contracts.Responses;
using BLL.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DrinkManager.API.Controllers
{
    public class UserController : BaseController
    {
        private readonly ILoginHandler _loginHandler;
        private readonly IRegisterHandler _registerHandler;

        public UserController(
            ILoginHandler loginHandler,
            IRegisterHandler registerHandler)
        {
            _loginHandler = loginHandler;
            _registerHandler = registerHandler;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {

            var user = await _loginHandler.Handle(request);

            if (user == null)
            {
                return Unauthorized("Login data is incorrect");
            }

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult<LoginResponse>> Register(RegisterRequest request)
        {

            var user = await _registerHandler.Handle(request);

            if (user == null)
            {
                return BadRequest("Username or Email already taken.");
            }

            return Ok(user);
        }
    }
}
