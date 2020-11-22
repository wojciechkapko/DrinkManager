using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DrinkManagerWeb.Models.ViewModels
{
    public class RoleViewModel
    {

        public Dictionary<string, List<string>> UsersPerRole { get; set; }

        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
