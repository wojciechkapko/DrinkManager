using System.Collections.Generic;
using BLL;

namespace DrinkManagerWeb.Models.ViewModels
{
    public class UserListViewModel
    {
        public Dictionary<string, List<string>> RolesPerUser { get; set; }
        public IEnumerable<AppUser> Users { get; set; }
    }
}
