using BLL.Contracts.Requests;
using BLL.Contracts.Responses;
using Domain;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BLL.Handlers
{
    public class LoginHandler : ILoginHandler
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LoginHandler(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<LoginResponse> Handle(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return null;
            }

            var loginResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);


            if (loginResult.Succeeded)
            {
                // todo: Generate token
                return new LoginResponse
                {
                    Username = user.UserName
                };
            }

            return null;
        }
    }
}
