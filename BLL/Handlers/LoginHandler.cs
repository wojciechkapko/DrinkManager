using BLL.Contracts.Requests;
using BLL.Contracts.Responses;
using BLL.Interfaces;
using Domain;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BLL.Handlers
{
    public class LoginHandler : ILoginHandler
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;

        public LoginHandler(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IJwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
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
                return new LoginResponse
                {
                    Username = user.UserName,
                    Token = await _jwtGenerator.CreateToken(user),
                    Role = (await _userManager.GetRolesAsync(user))[0]
                };
            }

            return null;
        }
    }
}
