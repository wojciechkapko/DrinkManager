using Domain;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DrinkManagerWeb.Models
{
    public class RoleEdit
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<AppUser> Members { get; set; }
        public IEnumerable<AppUser> NonMembers { get; set; }
    }
}
