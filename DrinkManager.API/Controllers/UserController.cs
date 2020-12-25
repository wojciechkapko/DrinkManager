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

        public UserController(ILoginHandler loginHandler)
        {
            _loginHandler = loginHandler;
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
    }
}
