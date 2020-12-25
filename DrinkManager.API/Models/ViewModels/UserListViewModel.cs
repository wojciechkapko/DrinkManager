using Domain;
using System.Collections.Generic;

namespace DrinkManagerWeb.Models.ViewModels
{
    public class UserListViewModel
    {
        public Dictionary<string, List<string>> RolesPerUser { get; set; }
        public IEnumerable<AppUser> Users { get; set; }
    }
}
