using BLL.Contracts.Requests;
using BLL.Contracts.Responses;
using BLL.Handlers;
using BLL.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DrinkManager.API.Controllers
{
    public class UserController : BaseController
    {
        private readonly ILoginHandler _loginHandler;
        private readonly IRegisterHandler _registerHandler;
        private readonly IUserAccessor _userAccessor;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly UserManager<AppUser> _userManager;

        public UserController(
            ILoginHandler loginHandler,
            IRegisterHandler registerHandler,
            IUserAccessor userAccessor,
            IJwtGenerator jwtGenerator,
            UserManager<AppUser> userManager)
        {
            _loginHandler = loginHandler;
            _registerHandler = registerHandler;
            _userAccessor = userAccessor;
            _jwtGenerator = jwtGenerator;
            _userManager = userManager;
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

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<LoginResponse>> CurrentUser()
        {
            var user = await _userManager.FindByNameAsync(_userAccessor.GetCurrentUsername());

            return Ok(new LoginResponse
            {
                Username = user.UserName,
                Token = _jwtGenerator.CreateToken(user),
                Image = null
            });
        }
    }
}
