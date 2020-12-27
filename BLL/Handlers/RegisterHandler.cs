using BLL.Contracts.Requests;
using BLL.Contracts.Responses;
using BLL.Interfaces;
using Domain;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BLL.Handlers
{
    public class RegisterHandler : IRegisterHandler
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtGenerator _jwtGenerator;

        public RegisterHandler(
            UserManager<AppUser> userManager,
            IJwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<LoginResponse> Handle(RegisterRequest request)
        {

            if (await _userManager.FindByEmailAsync(request.Email) != null || await _userManager.FindByNameAsync(request.Username) != null)
            {
                return null;
            }

            var newUser = new AppUser
            {
                UserName = request.Username,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "Employee");

                return new LoginResponse
                {
                    Username = newUser.UserName,
                    Token = await _jwtGenerator.CreateToken(newUser)
                };
            }

            return null;
        }
    }
}
