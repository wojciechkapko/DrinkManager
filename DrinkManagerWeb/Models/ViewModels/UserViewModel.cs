using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DrinkManagerWeb.Models.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        public string Password { get; set; }
        public List<SelectListItem> ApplicationRoles { get; set; }  
        [Display(Name = "Role")]  
        public string ApplicationRoleId { get; set; } 
    }
}
