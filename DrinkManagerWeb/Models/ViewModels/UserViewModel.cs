using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DrinkManagerWeb.Models.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password must be at least 6 and max 100 characters long, and must contain letter and digit.")]
        public string Password { get; set; }
        public List<SelectListItem> ApplicationRoles { get; set; }  
        [Display(Name = "Role")]  
        public string ApplicationRoleId { get; set; } 
    }
}
